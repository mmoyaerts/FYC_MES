namespace lireIndicateur
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBoxFormulaire = new GroupBox();
            button1 = new Button();
            comboBox1 = new ComboBox();
            label2 = new Label();
            label1 = new Label();
            dateTimePicker1 = new DateTimePicker();
            groupBox1 = new GroupBox();
            dataGridView1 = new DataGridView();
            label4 = new Label();
            label3 = new Label();
            groupBox2 = new GroupBox();
            label15 = new Label();
            label14 = new Label();
            label13 = new Label();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            groupBox3 = new GroupBox();
            label21 = new Label();
            label20 = new Label();
            label19 = new Label();
            label18 = new Label();
            label17 = new Label();
            label16 = new Label();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            label22 = new Label();
            label23 = new Label();
            groupBoxFormulaire.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxFormulaire
            // 
            groupBoxFormulaire.BackColor = Color.White;
            groupBoxFormulaire.Controls.Add(button1);
            groupBoxFormulaire.Controls.Add(comboBox1);
            groupBoxFormulaire.Controls.Add(label2);
            groupBoxFormulaire.Controls.Add(label1);
            groupBoxFormulaire.Controls.Add(dateTimePicker1);
            groupBoxFormulaire.Location = new Point(-2, 0);
            groupBoxFormulaire.Name = "groupBoxFormulaire";
            groupBoxFormulaire.Size = new Size(1294, 88);
            groupBoxFormulaire.TabIndex = 0;
            groupBoxFormulaire.TabStop = false;
            groupBoxFormulaire.Text = "Formulaire";
            // 
            // button1
            // 
            button1.Location = new Point(1110, 40);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 4;
            button1.Text = "Valider";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Matin", "Après-midi" });
            comboBox1.Location = new Point(783, 41);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(182, 33);
            comboBox1.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(623, 44);
            label2.Name = "label2";
            label2.Size = new Size(154, 25);
            label2.TabIndex = 2;
            label2.Text = "choix de l'équipe :";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(100, 44);
            label1.Name = "label1";
            label1.Size = new Size(145, 25);
            label1.TabIndex = 1;
            label1.Text = "choix de la date :";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(260, 39);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(300, 31);
            dateTimePicker1.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.RosyBrown;
            groupBox1.Controls.Add(dataGridView1);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Location = new Point(-2, 89);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(694, 377);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Production";
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(32, 53);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(644, 260);
            dataGridView1.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(316, 329);
            label4.Name = "label4";
            label4.Size = new Size(59, 25);
            label4.TabIndex = 1;
            label4.Text = "label4";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(171, 329);
            label3.Name = "label3";
            label3.Size = new Size(120, 25);
            label3.TabIndex = 0;
            label3.Text = "Performance :";
            // 
            // groupBox2
            // 
            groupBox2.BackColor = SystemColors.ActiveCaption;
            groupBox2.Controls.Add(label15);
            groupBox2.Controls.Add(label14);
            groupBox2.Controls.Add(label13);
            groupBox2.Controls.Add(label12);
            groupBox2.Controls.Add(label11);
            groupBox2.Controls.Add(label10);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(label5);
            groupBox2.Location = new Point(698, 89);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(594, 241);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "defaut poste";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(410, 202);
            label15.Name = "label15";
            label15.Size = new Size(69, 25);
            label15.TabIndex = 12;
            label15.Text = "label15";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(410, 147);
            label14.Name = "label14";
            label14.Size = new Size(69, 25);
            label14.TabIndex = 11;
            label14.Text = "label14";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(410, 93);
            label13.Name = "label13";
            label13.Size = new Size(69, 25);
            label13.TabIndex = 10;
            label13.Text = "label13";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(161, 202);
            label12.Name = "label12";
            label12.Size = new Size(69, 25);
            label12.TabIndex = 9;
            label12.Text = "label12";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(161, 147);
            label11.Name = "label11";
            label11.Size = new Size(69, 25);
            label11.TabIndex = 8;
            label11.Text = "label11";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(161, 93);
            label10.Name = "label10";
            label10.Size = new Size(69, 25);
            label10.TabIndex = 7;
            label10.Text = "label10";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(25, 202);
            label9.Name = "label9";
            label9.Size = new Size(72, 25);
            label9.TabIndex = 6;
            label9.Text = "poste 3";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(25, 147);
            label8.Name = "label8";
            label8.Size = new Size(72, 25);
            label8.TabIndex = 5;
            label8.Text = "poste 2";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(25, 93);
            label7.Name = "label7";
            label7.Size = new Size(72, 25);
            label7.TabIndex = 4;
            label7.Text = "poste 1";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(366, 53);
            label6.Name = "label6";
            label6.Size = new Size(127, 25);
            label6.TabIndex = 3;
            label6.Text = "temps d'arret :";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(125, 53);
            label5.Name = "label5";
            label5.Size = new Size(140, 25);
            label5.TabIndex = 2;
            label5.Text = "nombre d'arret :";
            // 
            // groupBox3
            // 
            groupBox3.BackColor = SystemColors.Info;
            groupBox3.Controls.Add(label21);
            groupBox3.Controls.Add(label20);
            groupBox3.Controls.Add(label19);
            groupBox3.Controls.Add(label18);
            groupBox3.Controls.Add(label17);
            groupBox3.Controls.Add(label16);
            groupBox3.Location = new Point(698, 336);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(594, 230);
            groupBox3.TabIndex = 3;
            groupBox3.TabStop = false;
            groupBox3.Text = "defaut produit";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(383, 186);
            label21.Name = "label21";
            label21.Size = new Size(69, 25);
            label21.TabIndex = 5;
            label21.Text = "label21";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(78, 179);
            label20.Name = "label20";
            label20.Size = new Size(69, 25);
            label20.TabIndex = 4;
            label20.Text = "label20";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(379, 126);
            label19.Name = "label19";
            label19.Size = new Size(69, 25);
            label19.TabIndex = 3;
            label19.Text = "label19";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(75, 116);
            label18.Name = "label18";
            label18.Size = new Size(69, 25);
            label18.TabIndex = 2;
            label18.Text = "label18";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(103, 41);
            label17.Name = "label17";
            label17.Size = new Size(69, 25);
            label17.TabIndex = 1;
            label17.Text = "label17";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(47, 41);
            label16.Name = "label16";
            label16.Size = new Size(50, 25);
            label16.TabIndex = 0;
            label16.Text = "FPY :";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(103, 504);
            label22.Name = "label22";
            label22.Size = new Size(51, 25);
            label22.TabIndex = 4;
            label22.Text = "TRS :";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(202, 508);
            label23.Name = "label23";
            label23.Size = new Size(69, 25);
            label23.TabIndex = 5;
            label23.Text = "label23";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1291, 567);
            Controls.Add(label23);
            Controls.Add(label22);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(groupBoxFormulaire);
            Name = "Form1";
            Text = "Form1";
            groupBoxFormulaire.ResumeLayout(false);
            groupBoxFormulaire.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBoxFormulaire;
        private Label label1;
        private DateTimePicker dateTimePicker1;
        private Label label2;
        private Button button1;
        private ComboBox comboBox1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label9;
        private Label label8;
        private Label label15;
        private Label label14;
        private Label label13;
        private Label label12;
        private Label label11;
        private Label label10;
        private Label label21;
        private Label label20;
        private Label label19;
        private Label label18;
        private Label label17;
        private Label label16;
        private Label label22;
        private Label label23;
        private DataGridView dataGridView1;
    }
}
