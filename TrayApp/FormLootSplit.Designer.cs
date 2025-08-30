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
            lblData = new Label();
            ((System.ComponentModel.ISupportInitialize)Divisao).BeginInit();
            SuspendLayout();
            // 
            // Divisao
            // 
            Divisao.AllowUserToAddRows = false;
            Divisao.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Divisao.Location = new Point(23, 43);
            Divisao.Name = "Divisao";
            Divisao.ReadOnly = true;
            Divisao.Size = new Size(537, 62);
            Divisao.TabIndex = 0;
            // 
            // lblData
            // 
            lblData.AutoSize = true;
            lblData.Location = new Point(23, 20);
            lblData.Name = "lblData";
            lblData.Size = new Size(38, 15);
            lblData.TabIndex = 1;
            lblData.Text = "label1";
            // 
            // FormLootSplit
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(583, 204);
            Controls.Add(lblData);
            Controls.Add(Divisao);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormLootSplit";
            Padding = new Padding(20);
            Text = "FormAutoShare";
            ((System.ComponentModel.ISupportInitialize)Divisao).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView Divisao;
        private Label lblData;
    }
}