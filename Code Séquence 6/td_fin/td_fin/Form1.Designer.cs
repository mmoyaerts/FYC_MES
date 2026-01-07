namespace td_fin
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            // Déclaration des contrôles
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnModifierOF = new System.Windows.Forms.Button();


            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit(); // Initialisation de DataGridView
            this.SuspendLayout();

            // 
            // dataGridView1 (Positionnement et propriétés)
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(10, 50);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(400, 300);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.ReadOnly = true; // Empêche la modification directe

            // 
            // panel1 (Positionnement et propriétés)
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(420, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(350, 150);
            this.panel1.TabIndex = 1;

            // 
            // btnModifierOF (Positionnement et événement Click pour l'Exercice 2)
            // 
            this.btnModifierOF.Location = new System.Drawing.Point(420, 10);
            this.btnModifierOF.Name = "btnModifierOF";
            this.btnModifierOF.Size = new System.Drawing.Size(150, 30);
            this.btnModifierOF.TabIndex = 2;
            this.btnModifierOF.Text = "Modifier OF (Ex. 2)";
            this.btnModifierOF.UseVisualStyleBackColor = true;

            this.btnModifierOF.Click += new System.EventHandler(this.btnModifierOF_Click);


            // 
            // Form1 (Configuration du formulaire principal)
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461); // Ajustement de la taille
            this.Controls.Add(this.btnModifierOF);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.Text = "TD Architecture et Persistence";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout(); // Important pour le redimensionnement
        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnModifierOF;
    }
}