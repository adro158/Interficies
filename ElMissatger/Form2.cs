using System;
using System.Drawing;
using System.Windows.Forms;

namespace ElMissatger
{
    public partial class Form2 : Form
    {
        private Label lblMissatge;

        public Form2(string missatgeRebut)
        {
            InitializeComponent();

            lblMissatge = new Label();
            lblMissatge.Text = "Hola, " + missatgeRebut;
            lblMissatge.AutoSize = true;
            lblMissatge.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblMissatge.Location = new Point(50, 50);

            this.Controls.Add(lblMissatge);
        }
    }
}