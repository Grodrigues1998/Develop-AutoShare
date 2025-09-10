using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace AutoShare.Services
{
    public static class UpdateService
    {
        private const string LatestInstallerUrl = "https://github.com/Grodrigues1998/AutoShare/releases/latest/download/AutoShareInstaller.exe";
        private const string LocalInstallerPath = "AutoShareInstaller.exe";

        private static readonly HttpClient httpClient = new HttpClient
        {
            DefaultRequestHeaders = { { "User-Agent", "AutoShareApp" } }
        };

        public static void CheckForUpdatesAsync(string currentVersion)
        {
            try
            {
                // Pega release mais recente no GitHub
                string apiUrl = "https://api.github.com/repos/Grodrigues1998/AutoShare/releases/latest";

                var version = httpClient.GetAsync(apiUrl).Result;
                using var release = JsonDocument.Parse(version.Content.ReadAsStringAsync().Result);

                string latestTag = release.RootElement.GetProperty("tag_name").GetString().Replace("v", "");

                if (Verificar(latestTag.Split("."), currentVersion.Split(".")))
                {
                    DialogResult result = MessageBox.Show(
                        $"Uma nova versão do AutoShare está disponível!\n\n" +
                        $"Versão atual: {currentVersion}\n" +
                        $"Nova versão: {latestTag}\n\n" +
                        $"Deseja atualizar agora?",
                        "Atualização disponível",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information
                    );

                    if (result == DialogResult.Yes)
                    {
                        DownloadAndInstallAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao verificar atualizações: " + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static bool Verificar(string[] release, string[] currentVersion)
        {
            for (int i = 0; i < release.Length; i++)
            {
                int prim = int.Parse(release[i]);
                int sec = int.Parse(currentVersion[i]);
                if (prim == sec)
                    continue;
                if (prim > sec)
                    return true;
                if (prim < sec)
                    return false;
            }
            return false;
        }

        private static void DownloadAndInstallAsync()
        {
            using (var response = httpClient.GetAsync(LatestInstallerUrl, HttpCompletionOption.ResponseHeadersRead).Result)
            {
                response.EnsureSuccessStatusCode();

                using var stream = response.Content.ReadAsStreamAsync().Result;
                using var fileStream = new FileStream(LocalInstallerPath, FileMode.Create, FileAccess.Write, FileShare.None);

                stream.CopyToAsync(fileStream).Wait();
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = LocalInstallerPath,
                UseShellExecute = true
            });

            Application.Exit();
        }
    }
}
