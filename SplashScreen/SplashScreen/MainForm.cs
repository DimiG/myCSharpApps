///<summary>
///SplashScreen Program - This code shows the splash screen in C# programs
///</summary>
///<remarks>
/// Program Name  : SplashScreen
/// Author     : DimiG
/// Requires   : .NET Framework 4.0 preinstalled, Windows XP SP3, Windows 7, Windows 8
///</remarks>

using System.Threading;
using System.Windows.Forms;

namespace SplashScreen
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            Thread thr = new Thread(new ThreadStart(SplashStart));
            thr.Start();
            Thread.Sleep(5000);
            InitializeComponent();
            thr.Abort();
        }

        public void SplashStart()
        {
            Application.Run(new SplashForm());
        }
    }
}
