using AutoShare.Domain;
using AutoShare.Models;
using AutoShare.Services.TrayApp;
using System.Diagnostics;
using System.Globalization;

namespace AutoShare.Services
{
    public static class PartyHuntService
    {
        private static string pastaDestino = Path.Combine(Utils.MainFolder, "Historico Party Analyzer");

        public static void Process(string texto, string personagem)
        {
            Utils.VerificarECriarPasta(pastaDestino);

            // Novo parser
            var session = ParseSession(texto, personagem);

            // Aqui você pode chamar o SplitLoot (se ainda precisar dele) passando os jogadores
            SplitLoot(session.Jogadores);

            ClipboardService.ClearClipboard();
            TrayAppService.TrayIcon.ShowBalloonTip(5000, "Loot repartido", "Clique aqui para obter mais detalhes!", ToolTipIcon.Info);

            SaveHistory(texto, session.Inicio, personagem);
        }

        public static PartyHuntSession ParseSession(string texto, string personagem)
        {
            var linhas = texto.Split("\n").Select(l => l.Trim()).Where(l => !string.IsNullOrEmpty(l)).ToList();

            var session = new PartyHuntSession();
            session.MeuPersonagem = personagem;
            // Sessão
            var header = linhas[0]; // "Session data: From ... to ..."
            session.Inicio = Utils.ConverterParaDateTime(header.Split("From ")[1].Split(" to ")[0]);
            session.Fim = Utils.ConverterParaDateTime(header.Split(" to ")[1]);
            session.Duracao = session.Fim - session.Inicio;

            session.LootType = linhas.First(x => x.StartsWith("Loot Type:")).Replace("Loot Type:", "").Trim();
            session.LootTotal = ExtrairValor(linhas.First(x => x.StartsWith("Loot:")), "Loot:");
            session.SuppliesTotal = ExtrairValor(linhas.First(x => x.StartsWith("Supplies:")), "Supplies:");
            session.BalanceTotal = ExtrairValor(linhas.First(x => x.StartsWith("Balance:")), "Balance:");

            // Jogadores (a partir da linha 6 normalmente, mas vamos detectar pelo Balance geral)
            int startIndex = linhas.FindIndex(l => l.StartsWith("Balance:")) + 1;

            for (int i = startIndex; i < linhas.Count; i++)
            {
                if (!linhas[i].Contains(":"))
                {
                    var nome = linhas[i].Replace("(Leader)", "").Trim();
                    bool isLeader = linhas[i].Contains("(Leader)");
                    var player = new PartyPlayer { Nome = nome, IsLeader = isLeader };

                    player.Loot = ExtrairValor(linhas[++i], "Loot:");
                    player.Supplies = ExtrairValor(linhas[++i], "Supplies:");
                    player.Balance = ExtrairValor(linhas[++i], "Balance:");
                    player.Damage = ExtrairValor(linhas[++i], "Damage:");
                    player.Healing = ExtrairValor(linhas[++i], "Healing:");

                    session.Jogadores.Add(player);
                }
            }

            return session;
        }

        private static long ExtrairValor(string linha, string chave)
        {
            return long.Parse(linha.Replace(chave, "").Replace(",", "").Trim());
        }

        private static void SaveHistory(string texto, DateTime data, string personagem)
        {
            string caminhoArquivo = Path.Combine(pastaDestino, $"{personagem} - {data:dd-MM-yyyyTHH-mm-ss}.txt");
            File.WriteAllText(caminhoArquivo, texto);
        }

        public static void MostrarUltimoPagamento()
        {
            Utils.VerificarECriarPasta(pastaDestino);

            var arquivo = new DirectoryInfo(pastaDestino)
                .GetFiles("*.txt")
                .OrderByDescending(f => f.CreationTime)
                .FirstOrDefault();

            if (arquivo == null)
            {
                MessageBox.Show("Nenhum pagamento calculado ainda.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var session = ParseSession(File.ReadAllText(arquivo.FullName), arquivo.FullName.Split("-")[0].Trim());
            var formLootSplit = new FormLootSplit(session);
            formLootSplit.Show();
        }

        public static List<string> SplitLoot(List<PartyPlayer> players)
        {
            long totalLoot = players.Sum(p => p.Loot);
            long totalSupplies = players.Sum(p => p.Supplies);
            long totalProfit = totalLoot - totalSupplies;
            long profitPerPlayer = totalProfit / players.Count;

            var payers = new List<(string Name, long Amount)>();
            var receivers = new List<(string Name, long Amount)>();

            foreach (var p in players)
            {
                long diff = profitPerPlayer - (p.Loot - p.Supplies);
                if (diff > 0) receivers.Add((p.Nome, diff));
                else if (diff < 0) payers.Add((p.Nome, -diff));
            }

            var pagamentos = new List<string>();
            int idxReceiver = 0;

            foreach (var payer in payers)
            {
                long amountLeft = payer.Amount;
                while (amountLeft > 0 && idxReceiver < receivers.Count)
                {
                    var receiver = receivers[idxReceiver];
                    long amountToTransfer = Math.Min(amountLeft, receiver.Amount);

                    pagamentos.Add($"{payer.Name} paga para {receiver.Name} {amountToTransfer}gps: transfer {amountToTransfer} to {receiver.Name}");

                    amountLeft -= amountToTransfer;
                    receivers[idxReceiver] = (receiver.Name, receiver.Amount - amountToTransfer);

                    if (receivers[idxReceiver].Amount == 0) idxReceiver++;
                }
            }
            return pagamentos;
        }
    }
}
