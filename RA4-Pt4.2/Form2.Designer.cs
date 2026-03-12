namespace RA4_Pt4._2
{
    partial class Form2
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
            dgvUsuaris = new DataGridView();
            btnNou = new Button();
            btnEditor = new Button();
            btnEliminar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvUsuaris).BeginInit();
            SuspendLayout();
            // 
            // dgvUsuaris
            // 
            dgvUsuaris.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsuaris.Location = new Point(29, 32);
            dgvUsuaris.Name = "dgvUsuaris";
            dgvUsuaris.Size = new Size(240, 150);
            dgvUsuaris.TabIndex = 0;
            // 
            // btnNou
            // 
            btnNou.Location = new Point(40, 221);
            btnNou.Name = "btnNou";
            btnNou.Size = new Size(75, 23);
            btnNou.TabIndex = 1;
            btnNou.Text = "Nou";
            btnNou.UseVisualStyleBackColor = true;
            btnNou.Click += btnNou_Click;
            // 
            // btnEditor
            // 
            btnEditor.Location = new Point(40, 272);
            btnEditor.Name = "btnEditor";
            btnEditor.Size = new Size(75, 23);
            btnEditor.TabIndex = 2;
            btnEditor.Text = "Editar";
            btnEditor.UseVisualStyleBackColor = true;
            btnEditor.Click += btnEditor_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(40, 320);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(75, 23);
            btnEliminar.TabIndex = 3;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnEliminar);
            Controls.Add(btnEditor);
            Controls.Add(btnNou);
            Controls.Add(dgvUsuaris);
            Name = "Form2";
            Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)dgvUsuaris).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvUsuaris;
        private Button btnNou;
        private Button btnEditor;
        private Button btnEliminar;
    }
}