using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Turmerik.LocalDiskExplorer.Background.WinForms.App
{
    public partial class MainForm : Form
    {
        private ToolStripButton closeTrayIconToolStripButton;
        private ContextMenuStrip trayContextMenuStrip;
        private NotifyIcon notifyIcon;

        public MainForm()
        {
            InitializeComponent();
            closeTrayIconToolStripButton = new ToolStripButton();

            closeTrayIconToolStripButton.Text = "Close";
            closeTrayIconToolStripButton.Click += CloseTrayIconToolStripButton_Click;

            trayContextMenuStrip = new ContextMenuStrip();
            trayContextMenuStrip.Items.Add(closeTrayIconToolStripButton);

            notifyIcon = new NotifyIcon();
            notifyIcon.ContextMenuStrip = trayContextMenuStrip;

            notifyIcon.MouseClick += NotifyIcon_MouseClick;

            notifyIcon.Icon = this.Icon;
            notifyIcon.Text = this.Text;

            notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon.BalloonTipTitle = this.Text;

            notifyIcon.BalloonTipText = string.Join(" ",
                "This icon helps the Turmerik Local Disk Explorer Background App",
                "(which, is responsible, for example, with showing the Windows Explorer context menu) to keep running.");

            notifyIcon.Visible = true;
        }

        private void NotifyIcon_MouseClick(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                notifyIcon.ShowBalloonTip(60 * 1000);
            }
        }

        private void CloseTrayIconToolStripButton_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
