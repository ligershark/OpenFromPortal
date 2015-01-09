[cmdletbinding(DefaultParameterSetName ='build')]
param(
    # actions
    [Parameter(ParameterSetName='build',Position=0)]
    [switch]$build,
    
    [Parameter(ParameterSetName='build',Position=1)]
    [string]$configuration = 'Release',

    [Parameter(ParameterSetName='build',Position=2)]
    [bool]$increaseVsixVersion = $false
)

function Get-ScriptDirectory
{
    $Invocation = (Get-Variable MyInvocation -Scope 1).Value
    Split-Path $Invocation.MyCommand.Path
}

$scriptDir = ((Get-ScriptDirectory) + "\")

function EnsurePsbuildInstalled{
    [cmdletbinding()]
    param()
    process{
        if(!(Get-Module -listAvailable 'psbuild')){
            (new-object Net.WebClient).DownloadString("https://raw.github.com/ligershark/psbuild/master/src/GetPSBuild.ps1") | iex

            if(!(Get-Module -listAvailable 'psbuild')){
                throw 'unable to download and load psbuild'
            }
        }

        if(!(Get-Module psbuild)){
            Import-Module psbuild -Global
        }

        if(!(Get-Module psbuild)){
            throw 'unable to download and load psbuild'
        }
    }
}

# begin script here

try{
    EnsurePsbuildInstalled
    Set-MSBuild "$env:windir\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe"

    $script:slnFilePath = ('{0}src\OpenFromPortal.sln' -f $scriptDir)

    Invoke-MSBuild .\src\OpenFromPortal.sln -configuration $configuration -visualStudioVersion 12.0 -properties @{
            'DeployExtension'='false'
            'UpdateVersion' = "$increaseVsixVersion"
        }
}
catch{
    throw ("Error:`n{0}" -f ($_.Exception.Message))
}
