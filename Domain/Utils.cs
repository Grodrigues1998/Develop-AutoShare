using System.Globalization;

namespace AutoShare.Domain
{
    internal static class Utils
    {
        public static string MainFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AutoShare");
        public static DateTime ConverterParaDateTime(string dataTexto)
        {
            // Define o formato da data
            string formato = "yyyy-MM-dd, HH:mm:ss";

            // Faz o parse exato
            return DateTime.ParseExact(dataTexto, formato, CultureInfo.InvariantCulture);
        }
        public static void VerificarECriarPasta(string directoryPath)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
                throw new ArgumentException("O caminho do diretório não pode ser nulo ou vazio.", nameof(directoryPath));

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
        }
    }

}
