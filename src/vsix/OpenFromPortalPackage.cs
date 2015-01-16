using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace LigerShark.OpenFromPortal
{

    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", Version, IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideAutoLoad(UIContextGuids80.NoSolution)]
    [ProvideAutoLoad(UIContextGuids80.SolutionExists)]
    [Guid(GuidList.guidOpenFromPortalPkgString)]
    public sealed class OpenFromPortalPackage : ExtensionPointPackage
    {
        public const string Version = "1.2";

        protected override void Initialize()
        {
            base.Initialize();

            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            CommandID cmd = new CommandID(GuidList.guidOpenFromPortalCmdSet, (int)PkgCmdIDList.cmdidMyCommand);
            OleMenuCommand button = new OleMenuCommand(ButtonClicked, cmd);
            mcs.AddCommand(button);
        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            string fileName = GetPublishSettings();

            if (!string.IsNullOrEmpty(fileName))
            {
                OpenProject(fileName);
            }
        }

        private void OpenProject(string fileName)
        {
            DTE2 dte = GetService(typeof(DTE)) as DTE2;
            Command cmd = dte.Commands.Item("File.OpenfromPortal");

            EnsurePublishPackage();

            if (cmd.IsAvailable)
            {
                dte.ExecuteCommand("File.OpenfromPortal", fileName);
            }
            else
            {
                MessageBox.Show("Web Development tools is not installed. Modify the Visual Studio installation to include it.", "Visual Studio", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        // The command File.OpenFromPortal is not available unless the publishing packgage have loaded.
        private void EnsurePublishPackage()
        {
            IVsShell vsShell = GetService(typeof(SVsShell)) as IVsShell;
            Guid guidPublishPkg = new Guid("{1ad387fc-b1e8-4023-91fe-f22260b661db}");
            IVsPackage publishPkg;
            vsShell.LoadPackage(ref guidPublishPkg, out publishPkg);
        }

        private static string GetPublishSettings()
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Publish Settings (*.PublishSettings)|*.PublishSettings";
                dialog.DefaultExt = ".PublishSettings";

                return dialog.ShowDialog() == DialogResult.OK ? dialog.FileName : null;
            }
        }
    }
}