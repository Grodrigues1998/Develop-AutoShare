using System.Diagnostics;
using System.Net;

namespace AutoShare.Services
{
    public static class UpdateService
    {
        private const string LatestInstallerUrl = "https://github.com/Grodrigues1998/AutoShare/releases/latest/download/AutoShareInstaller.exe";
        private const string LocalInstallerPath = "AutoShareInstaller.exe";

        public static void CheckForUpdates(string currentVersion)
        {
            // Usa GitHub API para pegar release mais recente
            using var client = new WebClient();
            client.Headers.Add("User-Agent", "AutoShareApp");
            string apiUrl = "https://api.github.com/repos/Grodrigues1998/AutoShare/releases/latest";

            string json = client.DownloadString(apiUrl);
            var release = System.Text.Json.JsonDocument.Parse(json);
            string latestTag = release.RootElement.GetProperty("tag_name").GetString();

            if (latestTag != $"v{currentVersion}")
            {
                DownloadAndInstall();
            }
        }

        private static void DownloadAndInstall()
        {
            using var client = new WebClient();
            client.DownloadFile(LatestInstallerUrl, LocalInstallerPath);
            Process.Start(LocalInstallerPath);
            System.Windows.Forms.Application.Exit();
        }
    }
}
