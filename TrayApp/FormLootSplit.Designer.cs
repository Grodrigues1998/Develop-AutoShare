using System.Windows.Forms;

namespace AutoShare
{
    partial class FormLootSplit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLootSplit));
            Divisao = new DataGridView();
            Data = new Label();
            Menu = new Panel();
            Estatisticas = new Button();
            Historico = new Button();
            Body = new Panel();
            panel2 = new Panel();
            panel1 = new Panel();
            Total = new Label();
            ((System.ComponentModel.ISupportInitialize)Divisao).BeginInit();
            Menu.SuspendLayout();
            Body.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // Divisao
            // 
            Divisao.AllowUserToAddRows = false;
            Divisao.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(249, 250, 251);
            Divisao.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            Divisao.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Divisao.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Divisao.BackgroundColor = Color.FromArgb(244, 246, 248);
            Divisao.BorderStyle = BorderStyle.None;
            Divisao.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Divisao.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(225, 228, 232);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(47, 59, 82);
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            Divisao.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            Divisao.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(51, 51, 51);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(214, 228, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(47, 59, 82);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            Divisao.DefaultCellStyle = dataGridViewCellStyle3;
            Divisao.Dock = DockStyle.Fill;
            Divisao.EnableHeadersVisualStyles = false;
            Divisao.Location = new Point(0, 0);
            Divisao.Margin = new Padding(0);
            Divisao.Name = "Divisao";
            Divisao.ReadOnly = true;
            Divisao.RowHeadersVisible = false;
            Divisao.Size = new Size(483, 319);
            Divisao.TabIndex = 0;
            // 
            // Data
            // 
            Data.AutoSize = true;
            Data.Location = new Point(11, 14);
            Data.Name = "Data";
            Data.Size = new Size(34, 15);
            Data.TabIndex = 1;
            Data.Text = "Data:";
            // 
            // Menu
            // 
            Menu.BackColor = Color.FromArgb(47, 59, 82);
            Menu.Controls.Add(Estatisticas);
            Menu.Controls.Add(Historico);
            Menu.Dock = DockStyle.Left;
            Menu.Location = new Point(0, 0);
            Menu.Margin = new Padding(0);
            Menu.Name = "Menu";
            Menu.Size = new Size(100, 361);
            Menu.TabIndex = 2;
            // 
            // Estatisticas
            // 
            Estatisticas.BackColor = Color.FromArgb(47, 59, 82);
            Estatisticas.FlatAppearance.BorderSize = 0;
            Estatisticas.FlatAppearance.MouseOverBackColor = Color.FromArgb(62, 74, 99);
            Estatisticas.FlatStyle = FlatStyle.Flat;
            Estatisticas.ForeColor = Color.White;
            Estatisticas.Image = Properties.Resources.Estatistica;
            Estatisticas.ImageAlign = ContentAlignment.MiddleLeft;
            Estatisticas.Location = new Point(0, 50);
            Estatisticas.Margin = new Padding(0);
            Estatisticas.Name = "Estatisticas";
            Estatisticas.Size = new Size(100, 50);
            Estatisticas.TabIndex = 0;
            Estatisticas.Text = "Estatistica";
            Estatisticas.TextAlign = ContentAlignment.MiddleRight;
            Estatisticas.TextImageRelation = TextImageRelation.ImageBeforeText;
            Estatisticas.UseVisualStyleBackColor = false;
            // 
            // Historico
            // 
            Historico.BackColor = Color.FromArgb(47, 59, 82);
            Historico.FlatAppearance.BorderSize = 0;
            Historico.FlatAppearance.MouseOverBackColor = Color.FromArgb(62, 74, 99);
            Historico.FlatStyle = FlatStyle.Flat;
            Historico.ForeColor = Color.White;
            Historico.Image = Properties.Resources.Historico;
            Historico.ImageAlign = ContentAlignment.MiddleLeft;
            Historico.Location = new Point(0, 0);
            Historico.Margin = new Padding(0);
            Historico.Name = "Historico";
            Historico.Size = new Size(100, 50);
            Historico.TabIndex = 0;
            Historico.Text = "Histórico";
            Historico.TextAlign = ContentAlignment.MiddleRight;
            Historico.TextImageRelation = TextImageRelation.ImageBeforeText;
            Historico.UseVisualStyleBackColor = false;
            // 
            // Body
            // 
            Body.Controls.Add(panel2);
            Body.Controls.Add(panel1);
            Body.Dock = DockStyle.Fill;
            Body.Location = new Point(100, 0);
            Body.Name = "Body";
            Body.Size = new Size(483, 361);
            Body.TabIndex = 3;
            // 
            // panel2
            // 
            panel2.Controls.Add(Divisao);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 42);
            panel2.Name = "panel2";
            panel2.Size = new Size(483, 319);
            panel2.TabIndex = 2;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(244, 246, 248);
            panel1.Controls.Add(Total);
            panel1.Controls.Add(Data);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(483, 42);
            panel1.TabIndex = 1;
            // 
            // Total
            // 
            Total.AutoSize = true;
            Total.Location = new Point(286, 14);
            Total.Name = "Total";
            Total.Size = new Size(36, 15);
            Total.TabIndex = 2;
            Total.Text = "Total:";
            // 
            // FormLootSplit
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(583, 361);
            Controls.Add(Body);
            Controls.Add(Menu);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormLootSplit";
            Text = "FormAutoShare";
            ((System.ComponentModel.ISupportInitialize)Divisao).EndInit();
            Menu.ResumeLayout(false);
            Body.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView Divisao;
        private Label Data;
        private Panel Menu;
        private Panel Body;
        private Panel panel1;
        private Button Historico;
        private Panel panel2;
        private Button Estatisticas;
        private Label Total;
    }
}