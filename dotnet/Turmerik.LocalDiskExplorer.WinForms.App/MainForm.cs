using System.Runtime.InteropServices;

namespace Turmerik.LocalDiskExplorer.WinForms.App
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            webView.Source = new Uri(Settings.Default.AppUrl);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}