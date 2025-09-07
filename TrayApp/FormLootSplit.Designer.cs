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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLootSplit));
            Divisao = new DataGridView();
            Data = new Label();
            Menu = new Panel();
            estatisticas = new Button();
            Split = new Button();
            Historico = new Button();
            BtnMenu = new Button();
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
            Divisao.BackgroundColor = SystemColors.Control;
            Divisao.BorderStyle = BorderStyle.None;
            Divisao.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Divisao.Dock = DockStyle.Fill;
            Divisao.Location = new Point(0, 0);
            Divisao.Margin = new Padding(0);
            Divisao.Name = "Divisao";
            Divisao.ReadOnly = true;
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
            Menu.Controls.Add(estatisticas);
            Menu.Controls.Add(Split);
            Menu.Controls.Add(Historico);
            Menu.Controls.Add(BtnMenu);
            Menu.Dock = DockStyle.Left;
            Menu.Location = new Point(0, 0);
            Menu.Margin = new Padding(0);
            Menu.Name = "Menu";
            Menu.Size = new Size(100, 361);
            Menu.TabIndex = 2;
            // 
            // estatisticas
            // 
            estatisticas.FlatAppearance.BorderSize = 0;
            estatisticas.FlatStyle = FlatStyle.Flat;
            estatisticas.Location = new Point(0, 150);
            estatisticas.Margin = new Padding(0);
            estatisticas.Name = "estatisticas";
            estatisticas.Size = new Size(100, 50);
            estatisticas.TabIndex = 0;
            estatisticas.Text = "Estatistica";
            estatisticas.UseVisualStyleBackColor = true;
            // 
            // Split
            // 
            Split.FlatAppearance.BorderSize = 0;
            Split.FlatStyle = FlatStyle.Flat;
            Split.Location = new Point(0, 100);
            Split.Margin = new Padding(0);
            Split.Name = "Split";
            Split.Size = new Size(100, 50);
            Split.TabIndex = 0;
            Split.Text = "Split";
            Split.UseVisualStyleBackColor = true;
            // 
            // Historico
            // 
            Historico.FlatAppearance.BorderSize = 0;
            Historico.FlatStyle = FlatStyle.Flat;
            Historico.Location = new Point(0, 50);
            Historico.Margin = new Padding(0);
            Historico.Name = "Historico";
            Historico.Size = new Size(100, 50);
            Historico.TabIndex = 0;
            Historico.Text = "Histórico";
            Historico.UseVisualStyleBackColor = true;
            // 
            // BtnMenu
            // 
            BtnMenu.FlatAppearance.BorderSize = 0;
            BtnMenu.FlatStyle = FlatStyle.Flat;
            BtnMenu.Location = new Point(0, 0);
            BtnMenu.Margin = new Padding(0);
            BtnMenu.Name = "BtnMenu";
            BtnMenu.Size = new Size(100, 50);
            BtnMenu.TabIndex = 0;
            BtnMenu.Text = "Menu";
            BtnMenu.UseVisualStyleBackColor = true;
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
            Total.Location = new Point(214, 14);
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
        private Button Split;
        private Button Historico;
        private Button BtnMenu;
        private Panel panel2;
        private Button estatisticas;
        private Label Total;
    }
}