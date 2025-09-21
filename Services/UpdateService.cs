using AutoShare.Properties;
using System.Diagnostics;
using System.Text.Json;

public static class UpdateService
{
    private const string LatestInstallerUrl = "https://github.com/Grodrigues1998/AutoShare/releases/latest/download/AutoShareInstaller.exe";
    private const string LocalInstallerPath = "AutoShareInstaller.exe";
    

    private static readonly HttpClient httpClient = new HttpClient
    {
        DefaultRequestHeaders = { { "User-Agent", "AutoShareApp" } }
    };

    private static System.Threading.Timer? timer;

    /// <summary>
    /// Inicia verificação automática de atualização a cada 10 minutos.
    /// </summary>
    public static void StartAutoUpdateCheck()
    {
        var currentVersion = Application.ProductVersion.Split("+")[0];
        if (Settings.Default.DisableUpdates) // substituto do Properties.Settings
            return;

        // Checa imediatamente de forma silenciosa
        Task.Run(() => CheckForUpdatesInternal(currentVersion, silent: true));

        // Agenda para rodar a cada 10 minutos
        timer = new System.Threading.Timer(_ =>
        {
            CheckForUpdatesInternal(currentVersion, silent: true);
        }, null, TimeSpan.FromMinutes(10), TimeSpan.FromMinutes(10));
    }

    /// <summary>
    /// Verificação manual — chamada pelo usuário.
    /// </summary>
    public static void CheckForUpdatesManually()
    {
        var currentVersion = Application.ProductVersion.Split("+")[0];
        CheckForUpdatesInternal(currentVersion, silent: false);
    }

    /// <summary>
    /// Lógica central de verificação de atualização.
    /// </summary>
    private static void CheckForUpdatesInternal(string currentVersion, bool silent)
    {
        try
        {
            if (silent && Settings.Default.DisableUpdates)
                return;

            string apiUrl = "https://api.github.com/repos/Grodrigues1998/AutoShare/releases/latest";
            var response = httpClient.GetAsync(apiUrl).Result;

            if (!response.IsSuccessStatusCode)
            {
                if (!silent)
                {
                    MessageBox.Show(
                        "Não foi possível verificar atualizações no momento.",
                        "AutoShare",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
                return;
            }

            using var release = JsonDocument.Parse(response.Content.ReadAsStringAsync().Result);
            string latestTag = release.RootElement.GetProperty("tag_name").GetString()?.Replace("v", "") ?? "";

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
                else if (silent)
                {
                    // Só grava recusa no modo automático
                    Settings.Default.DisableUpdates = true;
                    Settings.Default.Save();
                    timer?.Dispose();
                    timer = null;
                }
            }
            else if (!silent)
            {
                MessageBox.Show(
                    $"Você já está usando a versão mais recente ({currentVersion}).",
                    "AutoShare",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }
        catch (Exception ex)
        {
            if (!silent)
            {
                MessageBox.Show(
                    $"Erro ao verificar atualizações:\n{ex.Message}",
                    "AutoShare",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }

    private static bool Verificar(string[] release, string[] currentVersion)
    {
        for (int i = 0; i < release.Length; i++)
        {
            int prim = int.Parse(release[i]);
            int sec = int.Parse(currentVersion[i]);
            if (prim == sec) continue;
            if (prim > sec) return true;
            if (prim < sec) return false;
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
