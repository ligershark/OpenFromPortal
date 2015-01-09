
[string]$commitMsg = $env:APPVEYOR_REPO_COMMIT_MESSAGE
if($commitMsg -eq $null){ $commitMsg = '' }

$updateVersion = $true

if($env:APPVEYOR_REPO_TAG -eq $true){
    $updateVersion = $false
    'Skipping version update because a tag was detected, preserving existing vsix manifest version' | Write-Host
}
elseif($commitMsg.ToLowerInvariant().Contains('release') -eq $true){
    $updateVersion = $false
    'Skipping version update because the commit message contains "release", preserving existing vsix manifest version' | Write-Host
}

$env:UpdateVersion = $updateVersion

.\build.ps1