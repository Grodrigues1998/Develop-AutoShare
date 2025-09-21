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
    public partial class FormHistorico : Form
    {
        public FormHistorico()
        {
            InitializeComponent();
            var lista = new[] { "Artmaguinha", "Big Tunners", "Sunrise Bella","Garabambo" };
            checkedComboBox1.AddItems(lista);
        }
    }
}
