using System;
using System.Windows.Forms;
using System.Speech.Synthesis;


namespace TXT2Speech
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        SpeechSynthesizer reader = new SpeechSynthesizer();

        private void btnSpeak_Click(object sender, EventArgs e)
        {
            if (richTextBox.Text != "")
            {
                reader.Dispose();
                reader = new SpeechSynthesizer();
                reader.SpeakAsync(richTextBox.Text);
            }
            else MessageBox.Show("Please Enter Some Text First!");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (reader != null)
            {
                reader.Dispose();
            }
        }
         
    }
}
