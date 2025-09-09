using System.Globalization;

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
            var inicio = ConverterParaDateTime(datas.First().Trim());
            string fim = datas.Last().Trim();

            string caminhoArquivo = Path.Combine(pastaDestino, "HuntAnalyzer-" + inicio.ToString("dd-MM-yyyyThh-mm-ss") + ".txt");
            File.WriteAllText(caminhoArquivo, texto);
            clipboard.ClearClipboard();
            trayIcon.ShowBalloonTip(1000, "Hunt Analyzer processado", "Adicionado ao historico!", ToolTipIcon.Info);
        }
        public static DateTime ConverterParaDateTime(string dataTexto)
        {
            // Define o formato da data
            string formato = "yyyy-MM-dd, HH:mm:ss";

            // Faz o parse exato
            return DateTime.ParseExact(dataTexto, formato, CultureInfo.InvariantCulture);
        }
    }
}
