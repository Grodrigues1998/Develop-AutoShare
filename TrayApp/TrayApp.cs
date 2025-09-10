using AutoShare;
using AutoShare.Domain;
using AutoShare.Services;
using AutoShare.Services.TrayApp;

namespace AutoShareTray
{
    public class TrayApp : Form
    {

        private Thread workerThread;

        public TrayApp()
        {

            UpdateService.CheckForUpdatesAsync(Application.ProductVersion.Split("+")[0]);

            TrayAppService.TrayIcon.ShowBalloonTip(1000, "AutoShare iniciado", "Monitorando área de transferência.", ToolTipIcon.Info);

            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;

            workerThread = new Thread(TrayAppService.MonitorLoop) { IsBackground = true };
            workerThread.Start();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Visible = false;
        }
    }
}
