using System;
using System.ComponentModel.Design;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

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
            _dte = GetService(typeof(DTE)) as DTE2;

            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (null != mcs)
            {
                CommandID cmd = new CommandID(GuidList.guidOpenFromPortalCmdSet, (int)PkgCmdIDList.cmdidMyCommand);
                OleMenuCommand button = new OleMenuCommand(ButtonClicked, cmd);
                button.BeforeQueryStatus += button_BeforeQueryStatus;
                mcs.AddCommand(button);
            }
        }

        void button_BeforeQueryStatus(object sender, EventArgs e)
        {
            OleMenuCommand button = (OleMenuCommand)sender;

            if (_dte.Version == "14.0")
            {
                button.Text = "Open from Azure Websites...";
            }
        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            string fileName = GetPublishSettings();

            if (string.IsNullOrEmpty(fileName))
                return;

            OpenProject(fileName);
        }

        private void OpenProject(string fileName)
        {
            Command cmd = _dte.Commands.Item("File.OpenfromPortal");

            if (!cmd.IsAvailable)
            {
                LoadWebTools();
            }

            _dte.ExecuteCommand("File.OpenfromPortal", fileName);
        }

        // The command File.OpenFromPortal is not available unless the WebTools have loaded.
        private void LoadWebTools()
        {
            string assembly = Assembly.GetExecutingAssembly().Location;
            string folder = Path.GetDirectoryName(assembly);
            string file = Path.Combine(folder, "Resources\\project.csproj");

            _dte.ExecuteCommand("File.OpenProject", "\"" + file + "\"");
            _dte.Solution.Close();
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
