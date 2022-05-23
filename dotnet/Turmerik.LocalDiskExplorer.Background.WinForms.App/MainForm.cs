using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Turmerik.Core.Infrastucture;
using Turmerik.NetCore.Services;

namespace Turmerik.LocalDiskExplorer.Background.WinForms.App
{
    public partial class MainForm : Form
    {
        private readonly object syncRoot = new object();
        private volatile int closing;

        private ToolStripButton closeTrayIconToolStripButton;
        private ContextMenuStrip trayContextMenuStrip;
        private NotifyIcon notifyIcon;

        private ILocalDiskExplorerBackgroundApiClientMethodNameRetriever localDiskExplorerBackgroundApiClientMethodNameRetriever;
        private HubConnection localDiskExplorerBackgroundApiHubConnection;

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
            localDiskExplorerBackgroundApiClientMethodNameRetriever = ServiceProviderContainer.Instance.Value.Services.GetRequiredService<ILocalDiskExplorerBackgroundApiClientMethodNameRetriever>();
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

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                ConnectToLocalDiskExplorerBackgroundApiHub();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Connection to Local Disk Explorer Background Api failed");
                this.Close();
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        #region LocalDiskExplorerBackgroundApiHub

        private void ConnectToLocalDiskExplorerBackgroundApiHub()
        {
            string url = string.Concat(
                Settings.Default.BackgroundApiBaseUri,
                LocalDiskExplorerBackgroundApiH.MAIN_HUB_NAME);

            localDiskExplorerBackgroundApiHubConnection = new HubConnectionBuilder().WithUrl(url).Build();
            AttachLocalDiskExplorerBackgroundApiHubConnectionEventHandlers();

            localDiskExplorerBackgroundApiHubConnection.StartAsync();
        }

        private void AttachLocalDiskExplorerBackgroundApiHubConnectionEventHandlers()
        {
            localDiskExplorerBackgroundApiHubConnection.Closed += OnLocalDiskExplorerBackgroundApiHubConnectionClosed;

            localDiskExplorerBackgroundApiHubConnection.On<FsEntryData>(
                localDiskExplorerBackgroundApiClientMethodNameRetriever.OpenFolderInOSFileExplorer.Value,
                OnOpenFolderInOSFileExplorer);

            localDiskExplorerBackgroundApiHubConnection.On<FsEntryData>(
                localDiskExplorerBackgroundApiClientMethodNameRetriever.OpenFolderInOSFileExplorer.Value,
                OnOpenFolderInOSFileExplorer);

            localDiskExplorerBackgroundApiHubConnection.On<FsEntryData>(
                localDiskExplorerBackgroundApiClientMethodNameRetriever.OpenFolderInTrmrkFileExplorer.Value,
                OnOpenFolderInTrmrkFileExplorer);

            localDiskExplorerBackgroundApiHubConnection.On<FsEntryData>(
                localDiskExplorerBackgroundApiClientMethodNameRetriever.OpenFileInOSDefaultApp.Value,
                OnOpenFileInOSDefaultApp);

            localDiskExplorerBackgroundApiHubConnection.On<FsEntryData>(
                localDiskExplorerBackgroundApiClientMethodNameRetriever.OpenFileInOSDefaultTextEditor.Value,
                OnOpenFileInOSDefaultTextEditor);

            localDiskExplorerBackgroundApiHubConnection.On<FsEntryData>(
                localDiskExplorerBackgroundApiClientMethodNameRetriever.OpenFileInOSTrmrkTextEditor.Value,
                OnOpenFileInOSTrmrkTextEditor);
        }

        private async Task OnLocalDiskExplorerBackgroundApiHubConnectionClosed(Exception exc)
        {
            bool reconnect;

            lock (syncRoot)
            {
                reconnect = closing == 0;
            }

            if (reconnect)
            {
                await Task.Delay(new Random().Next(1, 5) * 1000);

                lock (syncRoot)
                {
                    reconnect = closing == 0;
                }

                if (reconnect && localDiskExplorerBackgroundApiHubConnection != null)
                {
                    await localDiskExplorerBackgroundApiHubConnection.StartAsync();
                }
            }
        }

        private void OnOpenFolderInOSFileExplorer(FsEntryData fsEntryData)
        {

        }

        private void OnOpenFolderInTrmrkFileExplorer(FsEntryData fsEntryData)
        {

        }

        private void OnOpenFileInOSDefaultApp(FsEntryData fsEntryData)
        {

        }

        private void OnOpenFileInOSDefaultTextEditor(FsEntryData fsEntryData)
        {

        }

        private void OnOpenFileInOSTrmrkTextEditor(FsEntryData fsEntryData)
        {

        }

        #endregion LocalDiskExplorerBackgroundApiHub
    }
}
