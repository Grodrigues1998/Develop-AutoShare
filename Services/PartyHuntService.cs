using AutoShare.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;

namespace AutoShare.Services
{
    public class PartyHuntService
    {
        private readonly string pastaDestino;
        private readonly NotifyIcon trayIcon;
        private readonly ClipboardService clipboard;
        public static List<string> UltimosPagamentos { get; private set; }

        public PartyHuntService(string pastaDestino, NotifyIcon trayIcon, ClipboardService clipboard)
        {
            this.pastaDestino = pastaDestino;
            this.trayIcon = trayIcon;
            this.clipboard = clipboard;
        }

        public void Process(string texto)
        {
            var (players, data) = ParsePlayers(texto);
            FileService.EnsureDirectoryExists(pastaDestino);
            SplitLoot(players);
            clipboard.ClearClipboard();
            trayIcon.ShowBalloonTip(5000, "Loot repartido", "Clique aqui para obter mais detalhes!", ToolTipIcon.Info);
            SaveHistory(texto, data);
        }

        public (List<Player>, DateTime) ParsePlayers(string texto)
        {
            var players = new List<Player>();
            var linhas = texto.Split("\r").Select(x => x.Trim()).ToList();
            int index = linhas.FindIndex(x => x.Contains("Balance: "));

            string nome = "";
            int loot = 0, sup = 0;
            DateTime data = ConverterParaDateTime(linhas.First().Split("From ")[1].Split(" to ")[0]);
            foreach (var linha in linhas.Skip(index + 1))
            {
                if (!linha.Contains(":"))
                    nome = linha.Replace("(Leader)", "").Trim();

                if (linha.Contains("Loot: "))
                    loot = int.Parse(linha.Replace("Loot: ", "").Replace(",", "").Trim());

                if (linha.Contains("Supplies: "))
                {
                    sup = int.Parse(linha.Replace("Supplies: ", "").Replace(",", "").Trim());
                    players.Add(new Player { Nome = nome, Loot = loot, Supplies = sup });
                }
            }
            return (players, data);
        }
        public static DateTime ConverterParaDateTime(string dataTexto)
        {
            // Define o formato da data
            string formato = "yyyy-MM-dd, HH:mm:ss";

            // Faz o parse exato
            return DateTime.ParseExact(dataTexto, formato, CultureInfo.InvariantCulture);
        }
        private void SaveHistory(string texto,DateTime data)
        {
            string caminhoArquivo = Path.Combine(pastaDestino, "PartyHunt");
            File.WriteAllText(caminhoArquivo + "-" + data.ToString("dd-MM-yyyyThh-mm-ss") + ".txt", texto);
        }

        public List<string> SplitLoot(List<Player> players)
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
            UltimosPagamentos = pagamentos;            
            return pagamentos;
        }
    }
}
