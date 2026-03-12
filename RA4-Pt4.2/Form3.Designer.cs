namespace RA4_Pt4._2
{
    partial class Form3
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
            txtNom = new TextBox();
            txtCorreu = new TextBox();
            cmbTipus = new ComboBox();
            lblExtra = new Label();
            txtExtra = new TextBox();
            btnGuardar = new Button();
            SuspendLayout();
            // 
            // txtNom
            // 
            txtNom.Location = new Point(50, 206);
            txtNom.Name = "txtNom";
            txtNom.Size = new Size(100, 23);
            txtNom.TabIndex = 0;
            txtNom.TextChanged += txtNom_TextChanged;
            // 
            // txtCorreu
            // 
            txtCorreu.Location = new Point(50, 250);
            txtCorreu.Name = "txtCorreu";
            txtCorreu.Size = new Size(100, 23);
            txtCorreu.TabIndex = 1;
            txtCorreu.TextChanged += txtCorreu_TextChanged;
            // 
            // cmbTipus
            // 
            cmbTipus.FormattingEnabled = true;
            cmbTipus.Items.AddRange(new object[] { "Estudiant", "Admin" });
            cmbTipus.Location = new Point(50, 304);
            cmbTipus.Name = "cmbTipus";
            cmbTipus.Size = new Size(121, 23);
            cmbTipus.TabIndex = 2;
            cmbTipus.SelectedIndexChanged += cmbTipus_SelectedIndexChanged;
            // 
            // lblExtra
            // 
            lblExtra.AutoSize = true;
            lblExtra.Location = new Point(219, 312);
            lblExtra.Name = "lblExtra";
            lblExtra.Size = new Size(38, 15);
            lblExtra.TabIndex = 3;
            lblExtra.Text = "label1";
            lblExtra.Click += lblExtra_Click;
            // 
            // txtExtra
            // 
            txtExtra.Location = new Point(206, 329);
            txtExtra.Name = "txtExtra";
            txtExtra.Size = new Size(100, 23);
            txtExtra.TabIndex = 4;
            txtExtra.TextChanged += txtExtra_TextChanged;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(295, 238);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(75, 23);
            btnGuardar.TabIndex = 5;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnGuardar);
            Controls.Add(txtExtra);
            Controls.Add(lblExtra);
            Controls.Add(cmbTipus);
            Controls.Add(txtCorreu);
            Controls.Add(txtNom);
            Name = "Form3";
            Text = "Form3";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtNom;
        private TextBox txtCorreu;
        private ComboBox cmbTipus;
        private Label lblExtra;
        private TextBox txtExtra;
        private Button btnGuardar;
    }
}