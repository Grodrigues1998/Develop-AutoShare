using AutoShare;
using AutoShare.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Windows.Forms;

namespace AutoShareTray
{
    public class TrayApp : Form
    {
        private readonly string pastaDestino = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AutoShare");
        private readonly string pastaHuntAnalyzer;
        private readonly string pastaPartyHunt;
        private readonly NotifyIcon trayIcon;
        private readonly ClipboardService clipboardService;
        private Thread workerThread;

        public TrayApp()
        {
            pastaHuntAnalyzer = Path.Combine(pastaDestino, "Historico Hunt Analyzer");
            pastaPartyHunt = Path.Combine(pastaDestino, "Historico Party Analyzer");

            // Verifica atualizações
            UpdateService.CheckForUpdates(Application.ProductVersion.Split("+")[0]);

            // Configura menu da bandeja
            var trayMenu = new ContextMenuStrip();
            trayMenu.Items.Add("Loot Split", null, OnLootSplit);
            trayMenu.Items.Add("Histórico", null, OnHistorico);
            trayMenu.Items.Add("Sair", null, OnExit);

            trayIcon = new NotifyIcon
            {
                Text = "AutoShare - Monitor de Loot",
                Icon = new System.Drawing.Icon("AutoShare.ico"),
                ContextMenuStrip = trayMenu,
                Visible = true
            };

            trayIcon.BalloonTipClicked += (s, e) => MostrarUltimosPagamentos();
            trayIcon.ShowBalloonTip(1000, "AutoShare iniciado", "Monitorando área de transferência.", ToolTipIcon.Info);

            clipboardService = new ClipboardService();

            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;

            workerThread = new Thread(MonitorLoop) { IsBackground = true };
            workerThread.Start();
        }

        private void MonitorLoop()
        {
            while (true)
            {
                try
                {
                    var texto = clipboardService.GetClipboardText();
                    if (!string.IsNullOrEmpty(texto) && texto.Contains("Session data: From"))
                    {
                        if (texto.Contains("XP Gain: "))
                            new HuntAnalyzerService(pastaHuntAnalyzer, trayIcon, clipboardService).Process(texto);
                        else
                            new PartyHuntService(pastaPartyHunt, trayIcon, clipboardService).Process(texto);
                    }
                }
                catch
                {
                    clipboardService.ClearClipboard();
                    trayIcon.ShowBalloonTip(2000, "Erro no processamento", "Formato inválido para Party Hunt ou Hunt Analyzer", ToolTipIcon.Error);
                }
                Thread.Sleep(100);
            }
        }

        private void MostrarUltimosPagamentos()
        {
            var arquivo = new DirectoryInfo(pastaPartyHunt)
                .GetFiles("*.txt")
                .OrderBy(f => f.CreationTime)
                .ToList();

            if (PartyHuntService.UltimosPagamentos != null && PartyHuntService.UltimosPagamentos.Count > 0)
            {
                var formPagamentos = new FormLootSplit(PartyHuntService.UltimosPagamentos, trayIcon);
                formPagamentos.Show();
            }
            else if (arquivo.Count > 0)
            {
                var service = new PartyHuntService(pastaPartyHunt, trayIcon, clipboardService);
                var ultimoparty = arquivo.First();
                var (players, data) = service.ParsePlayers(File.ReadAllText(ultimoparty.FullName));
                var pagamentos = service.SplitLoot(players);
                var formPagamentos = new FormLootSplit(pagamentos, trayIcon);
                formPagamentos.Show();
            }
            else
            {
                MessageBox.Show("Nenhum pagamento calculado ainda.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void OnLootSplit(object sender, EventArgs e) => MostrarUltimosPagamentos();

        private void OnHistorico(object sender, EventArgs e)
        {
            try
            {
                Directory.CreateDirectory(pastaDestino);
                System.Diagnostics.Process.Start("explorer.exe", pastaDestino);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Não foi possível abrir a pasta: {ex.Message}");
            }
        }

        private void OnExit(object sender, EventArgs e)
        {
            trayIcon.ShowBalloonTip(2000, "AutoShare finalizado", "O monitoramento foi encerrado.", ToolTipIcon.Info);
            trayIcon.Visible = false;
            Thread.Sleep(500);
            Application.Exit();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Visible = false;
        }
    }
}
