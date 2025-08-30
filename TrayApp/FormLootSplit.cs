using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoShare
{
    public partial class FormLootSplit : Form
    {
        private List<(string Texto, string Comando)> pagamentos;
        private readonly NotifyIcon TrayIcon;
        public FormLootSplit(List<string> pagamentosLista,NotifyIcon trayIcon)
        {
            TrayIcon = trayIcon;
            InitializeComponent();
            pagamentos = pagamentosLista
           .Select(p => (p.Split(':').First().Trim(), p.Split(':').Last().Trim())) // Texto antes e comando depois dos ":"
           .ToList();
            CriarTabela();

        }
        private void CriarTabela()
        {
            var trayMenu = new ContextMenuStrip();

            Divisao.Dock = DockStyle.Top;
            Divisao.AutoGenerateColumns = false;
            Divisao.AllowUserToAddRows = false;
            Divisao.ReadOnly = true;

            Divisao.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Pagamento",
                DataPropertyName = "Texto",
                Width = 400
            });

            Divisao.Columns.Add(new DataGridViewButtonColumn
            {
                HeaderText = "Ação",
                Text = "Copiar",
                UseColumnTextForButtonValue = true
            });

            // Bind dos dados
            Divisao.DataSource = pagamentos.Select(p => new { Texto = p.Texto }).ToList();

            // Evento de clique no botão
            Divisao.CellClick += (s, e) =>
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == 1)
                {
                    var comando = pagamentos[e.RowIndex].Comando;
                    Clipboard.SetText(comando);
                    TrayIcon.ShowBalloonTip(1000, "Transfer copiada", $"Transfer '{comando}' copiado para a área de transferência!", ToolTipIcon.Info);
                }
            };
        }
    }
}
