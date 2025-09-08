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

            string[] datas = linhas.First().Split("From")[1].Split("to");
            string inicio = datas.First().Trim();
            string fim = datas.Last().Trim();            

            string caminhoArquivo = Path.Combine(pastaDestino, "HuntAnalyzer.csv");
            File.AppendAllLines(caminhoArquivo, texto.Split("\n"));
            clipboard.ClearClipboard();
            trayIcon.ShowBalloonTip(1000, "Hunt Analyzer processado", "Adicionado ao historico!", ToolTipIcon.Info);
        }

        private string GetValor(List<string> linhas, string chave) =>
            linhas.First(x => x.Contains(chave)).Split(":")[1].Replace(",", "").Trim();
    }
}
