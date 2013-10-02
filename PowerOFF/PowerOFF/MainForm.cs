//<summary>
/// PowerOFF Program - This code helps poweroff your computer by WMI in C#
///</summary>
///<remarks>
/// Program Name  : PowerOFF
/// Author     : DimiG
/// Requires   : .NET Framework 4.0 preinstalled, Windows XP SP3, Windows 7 or Windows 8
///</remarks>

using System;
using System.Management;
using System.Windows.Forms;

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
            try
            {
                foreach (ManagementObject manObj in mcWin32.GetInstances())
                {
                    mboShutdown = manObj.InvokeMethod("Win32Shutdown",
                                                   mboShutdownParams, null);
                }
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private void btnPowerOff_Click(object sender, EventArgs e)
        {
            // Begin PowerOFF your computer
            Shutdown("1");
        }

        private void poweroffToolStripButton_Click(object sender, EventArgs e)
        {
            // Begin PowerOFF your computer
            //Process.Start("shutdown", "-s -t 3");
            Shutdown("1");
        }

        private void restartToolStripButton_Click(object sender, EventArgs e)
        {
            // Begin Restart your computer
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
