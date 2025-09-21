using AutoShare.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShare.Services.TrayApp
{
    public static class TrayAppService
    {
        public static NotifyIcon TrayIcon = CreateTrayIcon();
        public static void MonitorLoop()
        {
            while (true)
            {
                try
                {
                    var texto = ClipboardService.GetClipboardText();
                    if (!string.IsNullOrEmpty(texto) && texto.Contains("Session data: From"))
                    {
                        var personagem = "";
                        var processo = Process.GetProcesses().Where(x => x.ProcessName == "client").ToList();
                        if (processo.Any())
                        {
                            personagem = processo.First().MainWindowTitle.Split("-")[2].Trim();
                        }

                        if (texto.Contains("XP Gain: "))
                            HuntAnalyzerService.Process(texto, personagem);
                        else
                            PartyHuntService.Process(texto, personagem);
                    }
                }
                catch
                {
                    ClipboardService.ClearClipboard();
                    TrayIcon.ShowBalloonTip(2000, "Erro no processamento", "Formato inválido para Party Hunt ou Hunt Analyzer", ToolTipIcon.Error);
                }
                Thread.Sleep(100);
            }
        }

        private static NotifyIcon CreateTrayIcon()
        {
            var menu = new ContextMenuStrip();
            menu.Items.AddRange(new ToolStripItem[]
            {
                new ToolStripMenuItem("Loot Split", null, OnLootSplit),
                new ToolStripMenuItem("Histórico", null, OnHistorico),
                new ToolStripMenuItem("Verificar Atualizações", null, OnAtualizacao),
                new ToolStripMenuItem("Sair", null, OnExit)
            });

            var icon = new NotifyIcon
            {
                Text = "AutoShare - Monitor de Loot",
                Icon = new System.Drawing.Icon("AutoShare.ico"),
                ContextMenuStrip = menu,
                Visible = true
            };
            icon.BalloonTipClicked += (s, e) => PartyHuntService.MostrarUltimoPagamento();
            return icon;
        }
        private static void OnLootSplit(object sender, EventArgs e) => PartyHuntService.MostrarUltimoPagamento();

        private static void OnHistorico(object sender, EventArgs e)
        {
            try
            {
                Directory.CreateDirectory(Utils.MainFolder);
                System.Diagnostics.Process.Start("explorer.exe", Utils.MainFolder);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Não foi possível abrir a pasta: {ex.Message}");
            }
        }

        private static void OnExit(object sender, EventArgs e)
        {
            TrayIcon.ShowBalloonTip(2000, "AutoShare finalizado", "O monitoramento foi encerrado.", ToolTipIcon.Info);
            TrayIcon.Visible = false;
            Thread.Sleep(500);
            Application.Exit();
        }
        private static void OnAtualizacao(object sender, EventArgs e)
        {
            UpdateService.CheckForUpdatesManually();
        }

    }
}
