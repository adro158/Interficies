namespace HobbyMania
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
            pictureBox1 = new PictureBox();
            btnGris1 = new Button();
            btnSuavitzat1 = new Button();
            btnSegmentacio1 = new Button();
            btnContorn1 = new Button();
            btnAfegir1 = new Button();
            btnInforme1 = new Button();
            tbCerca1 = new TextBox();
            dataGridView1 = new DataGridView();
            btnCarregar1 = new Button();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(30, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(376, 243);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // btnGris1
            // 
            btnGris1.Location = new Point(132, 273);
            btnGris1.Name = "btnGris1";
            btnGris1.Size = new Size(75, 23);
            btnGris1.TabIndex = 1;
            btnGris1.Text = "Gris";
            btnGris1.UseVisualStyleBackColor = true;
            btnGris1.Click += btnGris1_Click;
            // 
            // btnSuavitzat1
            // 
            btnSuavitzat1.Location = new Point(132, 312);
            btnSuavitzat1.Name = "btnSuavitzat1";
            btnSuavitzat1.Size = new Size(75, 23);
            btnSuavitzat1.TabIndex = 2;
            btnSuavitzat1.Text = "Suavitzat";
            btnSuavitzat1.UseVisualStyleBackColor = true;
            // 
            // btnSegmentacio1
            // 
            btnSegmentacio1.Location = new Point(213, 273);
            btnSegmentacio1.Name = "btnSegmentacio1";
            btnSegmentacio1.Size = new Size(75, 23);
            btnSegmentacio1.TabIndex = 3;
            btnSegmentacio1.Text = "Segmentacio";
            btnSegmentacio1.UseVisualStyleBackColor = true;
            btnSegmentacio1.Click += btnSegmentacio1_Click;
            // 
            // btnContorn1
            // 
            btnContorn1.Location = new Point(213, 312);
            btnContorn1.Name = "btnContorn1";
            btnContorn1.Size = new Size(75, 23);
            btnContorn1.TabIndex = 4;
            btnContorn1.Text = "Contorn";
            btnContorn1.UseVisualStyleBackColor = true;
            btnContorn1.Click += btnContorn1_Click;
            // 
            // btnAfegir1
            // 
            btnAfegir1.Location = new Point(132, 358);
            btnAfegir1.Name = "btnAfegir1";
            btnAfegir1.Size = new Size(75, 23);
            btnAfegir1.TabIndex = 5;
            btnAfegir1.Text = "Afegir";
            btnAfegir1.UseVisualStyleBackColor = true;
            btnAfegir1.Click += btnAfegir1_Click;
            // 
            // btnInforme1
            // 
            btnInforme1.Location = new Point(683, 312);
            btnInforme1.Name = "btnInforme1";
            btnInforme1.Size = new Size(75, 23);
            btnInforme1.TabIndex = 6;
            btnInforme1.Text = "Informe";
            btnInforme1.UseVisualStyleBackColor = true;
            btnInforme1.Click += btnInforme1_Click;
            // 
            // tbCerca1
            // 
            tbCerca1.Location = new Point(520, 284);
            tbCerca1.Name = "tbCerca1";
            tbCerca1.Size = new Size(100, 23);
            tbCerca1.TabIndex = 7;
            tbCerca1.TextChanged += tbCerca1_TextChanged;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(454, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(334, 243);
            dataGridView1.TabIndex = 8;
            // 
            // btnCarregar1
            // 
            btnCarregar1.Location = new Point(21, 273);
            btnCarregar1.Name = "btnCarregar1";
            btnCarregar1.Size = new Size(75, 23);
            btnCarregar1.TabIndex = 9;
            btnCarregar1.Text = "Cargar";
            btnCarregar1.UseVisualStyleBackColor = true;
            btnCarregar1.Click += btnCarregar1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(402, 312);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 10;
            label1.Text = "label1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Controls.Add(btnCarregar1);
            Controls.Add(dataGridView1);
            Controls.Add(tbCerca1);
            Controls.Add(btnInforme1);
            Controls.Add(btnAfegir1);
            Controls.Add(btnContorn1);
            Controls.Add(btnSegmentacio1);
            Controls.Add(btnSuavitzat1);
            Controls.Add(btnGris1);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Button btnGris1;
        private Button btnSuavitzat1;
        private Button btnSegmentacio1;
        private Button btnContorn1;
        private Button btnAfegir1;
        private Button btnInforme1;
        private TextBox tbCerca1;
        private DataGridView dataGridView1;
        private Button btnCarregar1;
        private Label label1;
    }
}
