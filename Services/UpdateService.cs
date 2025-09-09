using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace AutoShare.Services
{
    public static class UpdateService
    {
        private const string LatestInstallerUrl = "https://github.com/Grodrigues1998/AutoShare/releases/latest/download/AutoShareInstaller.exe";
        private const string LocalInstallerPath = "AutoShareInstaller.exe";

        public static void CheckForUpdates(string currentVersion)
        {
            try
            {
                // Usa GitHub API para pegar release mais recente
                using var client = new WebClient();
                client.Headers.Add("User-Agent", "AutoShareApp");
                string apiUrl = "https://api.github.com/repos/Grodrigues1998/AutoShare/releases/latest";

                string json = client.DownloadString(apiUrl);
                var release = System.Text.Json.JsonDocument.Parse(json);
                string latestTag = release.RootElement.GetProperty("tag_name").GetString().Replace("v", "");

                if (verificar(latestTag.Split("."), currentVersion.Split(".")))
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
                        DownloadAndInstall();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao verificar atualizações: " + ex.Message,
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static bool verificar(string[] release, string[] currentVersion)
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

        private static void DownloadAndInstall()
        {
            using var client = new WebClient();
            client.DownloadFile(LatestInstallerUrl, LocalInstallerPath);
            Process.Start(LocalInstallerPath);
            System.Windows.Forms.Application.Exit();
        }
    }
}
