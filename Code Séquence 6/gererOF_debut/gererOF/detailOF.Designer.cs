namespace gererOF
{
    partial class detailOF
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            radioButton1 = new RadioButton();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Verdana", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(342, 24);
            label1.Name = "label1";
            label1.Size = new Size(195, 34);
            label1.TabIndex = 0;
            label1.Text = "OF numero :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Verdana", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(560, 24);
            label2.Name = "label2";
            label2.Size = new Size(35, 34);
            label2.TabIndex = 1;
            label2.Text = "0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Verdana", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(54, 111);
            label3.Name = "label3";
            label3.Size = new Size(297, 34);
            label3.TabIndex = 2;
            label3.Text = "Robot chargement :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Verdana", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(548, 111);
            label4.Name = "label4";
            label4.Size = new Size(246, 34);
            label4.TabIndex = 3;
            label4.Text = "Déchargement :";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Verdana", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(54, 304);
            label5.Name = "label5";
            label5.Size = new Size(294, 34);
            label5.TabIndex = 4;
            label5.Text = "Robot de gonflage :";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Verdana", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(357, 111);
            label6.Name = "label6";
            label6.Size = new Size(35, 34);
            label6.TabIndex = 5;
            label6.Text = "0";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Verdana", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(800, 111);
            label7.Name = "label7";
            label7.Size = new Size(35, 34);
            label7.TabIndex = 6;
            label7.Text = "0";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Verdana", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.Location = new Point(357, 304);
            label8.Name = "label8";
            label8.Size = new Size(35, 34);
            label8.TabIndex = 7;
            label8.Text = "0";
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(560, 310);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(178, 29);
            radioButton1.TabIndex = 8;
            radioButton1.TabStop = true;
            radioButton1.Text = "contrôle gonflage";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // detailOF
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(918, 462);
            Controls.Add(radioButton1);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "detailOF";
            Text = "detailOF";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private RadioButton radioButton1;
    }
}