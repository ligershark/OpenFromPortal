# Open from Azure Websites
### A Visual Studio extension

[![Build status](https://ci.appveyor.com/api/projects/status/1ixd3k8bjt092h8l?svg=true)](https://ci.appveyor.com/project/sayedihashimi/openfromportal)

__Open any web application hosted on Azure Websites directly in Visual Studio.__

Download the extension from the
[VS Gallery](https://visualstudiogallery.msdn.microsoft.com/60d414b1-4ead-4fde-9359-588aa126cd6c)
or get the
[nightly build](https://ci.appveyor.com/project/sayedihashimi/openfromportal/build/artifacts).

### Use this extension

#### Step 1: Download Publish Profile

All you need is your Azure Webistes publish settings file. You can get the file in two ways:

1. Download it from the Dashboard for the website on the Azure Portal online.
2. Use Server Explorer to find the Azure | Websites node and right-click to download.

#### Step 2: Open from Azure Websites

A new button appears in the `File` top menu as shown below.

![File Open](https://raw.githubusercontent.com/ligershark/OpenFromPortal/master/img/FileOpen.png)

Now all you have to do is click on it to select your .PublishSettings file.

#### Step 3: Download website content

You'll now be presented by the File Preview dialog that lets which files to open.

![Download files preview](https://raw.githubusercontent.com/ligershark/OpenFromPortal/master/img/PreviewDialog.png)

After downloading the files, the project opens in Visual Studio.

![Solution Explorer](https://raw.githubusercontent.com/ligershark/OpenFromPortal/master/img/SolutionExplorer.png)

#### Step 4: Share this with your friends

Now that you found this awesome extension, you should [Tweet about it](https://twitter.com/share?url=https%3a%2f%2fvisualstudiogallery.msdn.microsoft.com%2f60d414b1-4ead-4fde-9359-588aa126cd6c&text=Easiest+way+to+open+your+Azure+Websites+directly+in+Visual+Studio.+%23LigerShark).

*Created by Bill Hiebert, Sayed Ibrahim Hashimi and Mads Kristensen*