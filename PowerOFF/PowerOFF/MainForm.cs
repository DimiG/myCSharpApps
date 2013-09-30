using System;
using System.Windows.Forms;
using System.Management;

namespace PowerOFF
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        void Shutdown(string mode)
        {
            ManagementBaseObject mboShutdown = null;
            ManagementClass mcWin32 = new ManagementClass("Win32_OperatingSystem");
            mcWin32.Get();

            // You can't shutdown without security privileges
            mcWin32.Scope.Options.EnablePrivileges = true;
            ManagementBaseObject mboShutdownParams =
                     mcWin32.GetMethodParameters("Win32Shutdown");

            // Flag 1 means we want to shut down the system. Use "2" to reboot.
            mboShutdownParams["Flags"] = mode;
            mboShutdownParams["Reserved"] = "0";
            foreach (ManagementObject manObj in mcWin32.GetInstances())
            {
                mboShutdown = manObj.InvokeMethod("Win32Shutdown",
                                               mboShutdownParams, null);
            }
        }

        private void btnPowerOff_Click(object sender, EventArgs e)
        {
            // Start PowerOFF your computer
            Shutdown("1");
        }

        private void poweroffToolStripButton_Click(object sender, EventArgs e)
        {
            // Start PowerOFF your computer
            //Process.Start("shutdown", "-s -t 3");
            Shutdown("1");
        }

        private void restartToolStripButton_Click(object sender, EventArgs e)
        {
            // Start Restart your computer
            Shutdown("2");
        }

        private void aboutToolStripButton_Click(object sender, EventArgs e)
        {
            // Show about box
            AboutBox AboutBoxForm = new AboutBox();
            AboutBoxForm.ShowDialog();
        }

    }
}
