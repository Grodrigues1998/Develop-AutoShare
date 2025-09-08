using System;
using System.IO;
using System.Linq;

namespace AutoShare.Services
{
    public static class FileService
    {
        public static void EnsureDirectoryExists(string directoryPath)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
                throw new ArgumentException("O caminho do diretório não pode ser nulo ou vazio.", nameof(directoryPath));

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
        }
    }
}
