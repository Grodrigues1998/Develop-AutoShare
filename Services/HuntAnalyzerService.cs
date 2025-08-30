using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AutoShare.Services;

namespace AutoShare.Services
{
    public class HuntAnalyzerService
    {
        private const string HEADER = "Inicio;Fim;Duracao;XP;Loot;Suply;Balance;Damage;Healing";
        private readonly string pastaDestino;
        private readonly NotifyIcon trayIcon;
        private readonly ClipboardService clipboard;

        public HuntAnalyzerService(string pastaDestino, NotifyIcon trayIcon, ClipboardService clipboard)
        {
            this.pastaDestino = pastaDestino;
            this.trayIcon = trayIcon;
            this.clipboard = clipboard;
        }

        public void Process(string texto)
        {
            var linhas = texto.Split("\n").Select(x => x.Trim()).ToList();
            FileService.EnsureDirectoryExists(pastaDestino);
            string caminhoArquivo = Path.Combine(pastaDestino, "HuntAnalyzer.csv");
            FileService.EnsureFileWithHeader(caminhoArquivo, HEADER);

            string[] datas = linhas.First().Split("From")[1].Split("to");
            string inicio = datas.First().Trim();
            string fim = datas.Last().Trim();
            var duracao = linhas.First(x => x.Contains("Session: ")).Split(":");
            string Duracao = $"{duracao[1]}:{duracao[2]}";

            string processado = $"{inicio};{fim};{Duracao};{GetValor(linhas, "XP Gain: ")};{GetValor(linhas, "Loot: ")};" +
                                $"{GetValor(linhas, "Supplies: ")};{GetValor(linhas, "Balance: ")};{GetValor(linhas, "Damage: ")};{GetValor(linhas, "Healing: ")}";

            File.AppendAllLines(caminhoArquivo, new[] { processado });
            clipboard.ClearClipboard();
            trayIcon.ShowBalloonTip(1000, "Hunt Analyzer processado", "Adicionado ao historico!", ToolTipIcon.Info);
        }

        private string GetValor(List<string> linhas, string chave) =>
            linhas.First(x => x.Contains(chave)).Split(":")[1].Replace(",", "").Trim();
    }
}
