using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoShare.Models
{
    public class PartyHuntSession
    {
        public string MeuPersonagem { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public TimeSpan Duracao { get; set; }
        public string LootType { get; set; }
        public long LootTotal { get; set; }
        public long SuppliesTotal { get; set; }
        public long BalanceTotal { get; set; }
        public List<PartyPlayer> Jogadores { get; set; } = new();
    }

    public class PartyPlayer
    {
        public string Nome { get; set; }
        public bool IsLeader { get; set; }
        public long Loot { get; set; }
        public long Supplies { get; set; }
        public long Balance { get; set; }
        public long Damage { get; set; }
        public long Healing { get; set; }
    }
}
