using AutoShare.Services.TrayApp;
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
        public FormLootSplit(List<string> pagamentosLista)
        {
            InitializeComponent();
            pagamentos = pagamentosLista
           .Select(p => (p.Split(':').First().Trim(), p.Split(':').Last().Trim())) // Texto antes e comando depois dos ":"
           .ToList();
            CriarTabela();

        }
        private void CriarTabela()
        {
            var trayMenu = new ContextMenuStrip();

            Divisao.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Pagar",
                DataPropertyName = "Texto",

            });

            Divisao.Columns.Add(new DataGridViewImageColumn
            {
                HeaderText = "Copiar",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = 70,
                Image = Image.FromFile(Path.Combine(Application.StartupPath, "img", "Copiar.png"))
            });

            // Bind dos dados
            Divisao.DataSource = pagamentos.Select(p => new { Texto = p.Texto }).ToList();

            // Evento de clique no botão
            Divisao.CellClick += (s, e) =>
            {
                if (e.ColumnIndex == 0)
                {
                    var comando = pagamentos[e.RowIndex].Comando;
                    Clipboard.SetText(comando);
                    TrayAppService.TrayIcon.ShowBalloonTip(1000, "Transfer copiada", $"Transfer copiado para a área de transferência!", ToolTipIcon.Info);
                }
            };
        }
    }
}
