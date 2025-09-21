using System.Windows.Forms;

namespace AutoShare
{
    partial class FormHistorico
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHistorico));
            Menu = new Panel();
            Estatisticas = new Button();
            Historico = new Button();
            Body = new Panel();
            panel1 = new Panel();
            checkedComboBox1 = new AutoShare.Components.CheckedComboBox();
            dtFim = new DateTimePicker();
            dtInicio = new DateTimePicker();
            lblFim = new Label();
            lblInicio = new Label();
            lblPersonagem = new Label();
            Menu.SuspendLayout();
            Body.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
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
            Menu.Size = new Size(100, 494);
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
            Historico.Image = (Image)resources.GetObject("Historico.Image");
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
            Body.Controls.Add(panel1);
            Body.Dock = DockStyle.Fill;
            Body.Location = new Point(100, 0);
            Body.Name = "Body";
            Body.Size = new Size(654, 494);
            Body.TabIndex = 3;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(244, 246, 248);
            panel1.Controls.Add(checkedComboBox1);
            panel1.Controls.Add(dtFim);
            panel1.Controls.Add(dtInicio);
            panel1.Controls.Add(lblFim);
            panel1.Controls.Add(lblInicio);
            panel1.Controls.Add(lblPersonagem);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(654, 71);
            panel1.TabIndex = 1;
            // 
            // checkedComboBox1
            // 
            checkedComboBox1.DrawMode = DrawMode.OwnerDrawFixed;
            checkedComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            checkedComboBox1.Location = new Point(15, 30);
            checkedComboBox1.Name = "checkedComboBox1";
            checkedComboBox1.Size = new Size(199, 24);
            checkedComboBox1.TabIndex = 7;
            // 
            // dtFim
            // 
            dtFim.CustomFormat = "dd/MM/yyyy";
            dtFim.Enabled = false;
            dtFim.Format = DateTimePickerFormat.Custom;
            dtFim.Location = new Point(351, 30);
            dtFim.MaxDate = new DateTime(2025, 9, 21, 0, 0, 0, 0);
            dtFim.Name = "dtFim";
            dtFim.Size = new Size(125, 23);
            dtFim.TabIndex = 4;
            dtFim.Value = new DateTime(2025, 9, 21, 0, 0, 0, 0);
            // 
            // dtInicio
            // 
            dtInicio.CustomFormat = "dd/MM/yyyy";
            dtInicio.Format = DateTimePickerFormat.Custom;
            dtInicio.Location = new Point(220, 30);
            dtInicio.MaxDate = new DateTime(2025, 9, 21, 0, 0, 0, 0);
            dtInicio.Name = "dtInicio";
            dtInicio.Size = new Size(125, 23);
            dtInicio.TabIndex = 3;
            dtInicio.Value = new DateTime(2025, 9, 21, 0, 0, 0, 0);
            // 
            // lblFim
            // 
            lblFim.AutoSize = true;
            lblFim.Location = new Point(351, 10);
            lblFim.Name = "lblFim";
            lblFim.Size = new Size(27, 15);
            lblFim.TabIndex = 2;
            lblFim.Text = "Fim";
            // 
            // lblInicio
            // 
            lblInicio.AutoSize = true;
            lblInicio.Location = new Point(220, 10);
            lblInicio.Name = "lblInicio";
            lblInicio.Size = new Size(36, 15);
            lblInicio.TabIndex = 1;
            lblInicio.Text = "Inicio";
            // 
            // lblPersonagem
            // 
            lblPersonagem.AutoSize = true;
            lblPersonagem.Location = new Point(15, 10);
            lblPersonagem.Name = "lblPersonagem";
            lblPersonagem.Size = new Size(73, 15);
            lblPersonagem.TabIndex = 0;
            lblPersonagem.Text = "Personagem";
            // 
            // FormHistorico
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(754, 494);
            Controls.Add(Body);
            Controls.Add(Menu);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormHistorico";
            Text = "FormAutoShare";
            Menu.ResumeLayout(false);
            Body.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label Data;
        private Panel Menu;
        private Panel Body;
        private Panel panel1;
        private Button Historico;
        private Button Estatisticas;
        private Label Total;
        private Label lblFim;
        private Label lblInicio;
        private Label lblPersonagem;
        private DateTimePicker dtFim;
        private DateTimePicker dtInicio;
        private Components.CheckedComboBox checkedComboBox1;
    }
}