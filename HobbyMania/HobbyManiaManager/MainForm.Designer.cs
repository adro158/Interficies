namespace HobbyManiaManager
{
    partial class MainForm
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelMoviesCounter = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelMoviesCounter
            // 
            this.labelMoviesCounter.AutoSize = true;
            this.labelMoviesCounter.Location = new System.Drawing.Point(13, 431);
            this.labelMoviesCounter.Name = "labelMoviesCounter";
            this.labelMoviesCounter.Size = new System.Drawing.Size(35, 13);
            this.labelMoviesCounter.TabIndex = 0;
            this.labelMoviesCounter.Text = "label1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelMoviesCounter);
            this.Name = "MainForm";
            this.Text = "Hobby Mania Manager";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelMoviesCounter;
    }
}

