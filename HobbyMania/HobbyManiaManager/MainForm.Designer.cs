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
            this.btnMovies = new System.Windows.Forms.Button();
            this.btnCustomers = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
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
            // btnMovies
            // 
            this.btnMovies.Location = new System.Drawing.Point(142, 176);
            this.btnMovies.Name = "btnMovies";
            this.btnMovies.Size = new System.Drawing.Size(128, 23);
            this.btnMovies.TabIndex = 1;
            this.btnMovies.Text = "Catalogo de Peliculas";
            this.btnMovies.UseVisualStyleBackColor = true;
            this.btnMovies.Click += new System.EventHandler(this.btnMovies_Click);
            // 
            // btnCustomers
            // 
            this.btnCustomers.Location = new System.Drawing.Point(142, 230);
            this.btnCustomers.Name = "btnCustomers";
            this.btnCustomers.Size = new System.Drawing.Size(128, 23);
            this.btnCustomers.TabIndex = 2;
            this.btnCustomers.Text = "Gestion de Clientes";
            this.btnCustomers.UseVisualStyleBackColor = true;
            this.btnCustomers.Click += new System.EventHandler(this.btnCustomers_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(142, 290);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(75, 23);
            this.btnHelp.TabIndex = 3;
            this.btnHelp.Text = "Ajuda";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnCustomers);
            this.Controls.Add(this.btnMovies);
            this.Controls.Add(this.labelMoviesCounter);
            this.Name = "MainForm";
            this.Text = "Hobby Mania Manager";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelMoviesCounter;
        private System.Windows.Forms.Button btnMovies;
        private System.Windows.Forms.Button btnCustomers;
        private System.Windows.Forms.Button btnHelp;
    }
}

