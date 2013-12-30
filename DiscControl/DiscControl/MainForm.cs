//<summary>
/// DiscControl Program - This code eject or close DVD tray on your computer by WinAPI in C#
///</summary>
///<remarks>
/// Program Name  : DiscControl
/// Author     : DimiG
/// Requires   : .NET Framework 4.0 preinstalled, Windows XP SP3, Windows 7 or Windows 8
///</remarks>

using System;
using System.Runtime.InteropServices; // Need for DLL import
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;

namespace DiscControl
{
    public partial class MainForm : Form
    {
        // Create a method that uses the WinAPI

        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", CharSet = CharSet.Ansi)]
        protected static extern int mciSendString(string lpstrCommand, StringBuilder lpstrReturnString, int uReturnLength, IntPtr hwndCallback);

        public MainForm()
        {
            InitializeComponent();
        }

        public bool ProcessCDTray(bool open)
        {
            /* Main method for tray control */

            int ret = 0;
            //do switch of the value passed
            switch (open)
            {
                case true:
                    ret = mciSendString("set cdaudio door open", null, 0, IntPtr.Zero);
                    return true;

                case false:
                    ret = mciSendString("set cdaudio door closed", null, 0, IntPtr.Zero);
                    return true;

                default:
                    ret = mciSendString("set cdaudio door open", null, 0, IntPtr.Zero);
                    return true;
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            /* Open button click method */
            // Hook up event handlers
            bgwDoOpenWork.RunWorkerCompleted += bgwDoOpenWork_RunWorkerCompleted;

            btnOpen.Enabled = false;
            toolStripProgressBar1.Visible = true;
            lblDisplay.Text = "Disc Tray Opening...";
            bgwDoOpenWork.RunWorkerAsync();
            showProgressBar();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            /* Close button click method */
            // Hook up event handlers
            bgwDoCloseWork.RunWorkerCompleted += bgwDoCloseWork_RunWorkerCompleted;
            
            btnClose.Enabled = false;
            toolStripProgressBar1.Visible = true;
            lblDisplay.Text = "Disc Tray Closing...";
            bgwDoCloseWork.RunWorkerAsync();
            showProgressBar();
        }

        private void toolStripBtn_Click(object sender, EventArgs e)
        {
            // Show about box
            AboutBox AboutBoxForm = new AboutBox();
            AboutBoxForm.ShowDialog();
        }

        private void bgwDoCloseWork_DoWork(object sender, DoWorkEventArgs e)
        {
            /* Background worker for Close */

            ProcessCDTray(false);
            
            //Thread.Sleep(700);
            //Debug.WriteLine("Do close CD tray");
        }

        private void bgwDoOpenWork_DoWork(object sender, DoWorkEventArgs e)
        {
            /* Background worker for Open */

            ProcessCDTray(true);

            //Thread.Sleep(700);
            //Debug.WriteLine("Do open CD tray");
        }

        private void showProgressBar()
        {
            /* Show progress bar */

            int total = 33; //some number (this is variable to change)!!

            for (int i = 0; i <= total; i++) //some number (total)
            {
                int percents = (i * 100) / total;
                toolStripProgressBar1.Value = percents;
            }
        }

        private void bgwDoOpenWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            /* Background worker for Open completed */

            btnOpen.Enabled = true;
            toolStripProgressBar1.Visible = false;
            lblDisplay.Text = "Disc Tray Opened";

            Debug.WriteLine("Completed");
        }

        private void bgwDoCloseWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            /* Background worker for Close completed */

            btnClose.Enabled = true;
            toolStripProgressBar1.Visible = false;
            lblDisplay.Text = "Disc Tray Closed";

            Debug.WriteLine("Completed");
        }
    }
}
