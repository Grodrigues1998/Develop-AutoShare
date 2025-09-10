using AutoShare.Domain;
using AutoShare.Services.TrayApp;
using System.Globalization;

namespace AutoShare.Services
{
    public static class HuntAnalyzerService
    {
        private static string pastaDestino = Path.Combine(Utils.MainFolder, "Historico Hunt Analyzer");
        public static void Process(string texto)
        {
            var linhas = texto.Split("\n").Select(x => x.Trim()).ToList();
            Utils.VerificarECriarPasta(pastaDestino);

            string[] datas = linhas.First().Split("From")[1].Split("to");
            var inicio = Utils.ConverterParaDateTime(datas.First().Trim());
            string fim = datas.Last().Trim();

            string caminhoArquivo = Path.Combine(pastaDestino, "HuntAnalyzer-" + inicio.ToString("dd-MM-yyyyThh-mm-ss") + ".txt");
            File.WriteAllText(caminhoArquivo, texto);
            ClipboardService.ClearClipboard();
            TrayAppService.TrayIcon.ShowBalloonTip(1000, "Hunt Analyzer processado", "Adicionado ao historico!", ToolTipIcon.Info);
        }
    }
}
