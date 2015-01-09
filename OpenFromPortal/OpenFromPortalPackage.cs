using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Microsoft.Win32;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using System.Windows.Forms;
using EnvDTE;
using EnvDTE80;
using System.IO;

namespace Company.OpenFromPortal
{

    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideAutoLoad(UIContextGuids80.NoSolution)]
    [ProvideAutoLoad(UIContextGuids80.SolutionExists)]
    [Guid(GuidList.guidOpenFromPortalPkgString)]
    public sealed class OpenFromPortalPackage : ExtensionPointPackage
    {
        private static DTE2 _dte;

        protected override void Initialize()
        {
            base.Initialize();
            _dte = ServiceProvider.GlobalProvider.GetService(typeof(DTE)) as DTE2;

            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (null != mcs)
            {
                CommandID menuCommandID = new CommandID(GuidList.guidOpenFromPortalCmdSet, (int)PkgCmdIDList.cmdidMyCommand);
                MenuCommand menuItem = new MenuCommand(ButtonClicked, menuCommandID);
                mcs.AddCommand(menuItem);
            }
        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            string fileName = GetPublishSettings();

            if (string.IsNullOrEmpty(fileName))
                return;

            //CreateEmptySolution();
            OpenProject(fileName);
        }

        private void OpenProject(string fileName)
        {
            Command cmd = _dte.Commands.Item("File.OpenfromPortal");

            if (cmd.IsAvailable)
            {
                _dte.ExecuteCommand("File.OpenfromPortal", fileName);
            }
        }

        private void CreateEmptySolution()
        {
            if (_dte.Solution.IsOpen)
            {
                _dte.Solution.Close(true);
            }

            string solutionName = "MyAzureWebsite.sln";
            string rootFolder = (string)UserRegistryRoot.GetValue("VisualStudioProjectsLocation");
            string projectFolder = Path.Combine(rootFolder, "AzureWebSite-" + DateTime.UtcNow.Ticks);
            string solutionFile = Path.Combine(projectFolder, solutionName);

            if (!Directory.Exists(projectFolder))
            {
                Directory.CreateDirectory(projectFolder);
            }

            _dte.Solution.Create(projectFolder, solutionName);
            _dte.Solution.SaveAs(solutionFile);
        }

        private static string GetPublishSettings()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Publish Settings (*.PublishSettings)|*.PublishSettings";
            dialog.DefaultExt = ".PublishSettings";

            if (dialog.ShowDialog() != DialogResult.OK)
                return null;

            return dialog.FileName;
        }
    }
}
