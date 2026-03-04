using System;
using System.Drawing;
using System.Windows.Forms;

namespace Controls_Fantasma
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Label etiquetaEstat = new Label();
            etiquetaEstat.Text = "Esperant acció...";
            etiquetaEstat.AutoSize = true;

            Button botoAccio = new Button();
            botoAccio.Text = "Clica'm";
            botoAccio.Width = 100;
            botoAccio.Height = 30;

            int centreX = (this.ClientSize.Width - botoAccio.Width) / 2;
            int centreY = (this.ClientSize.Height - botoAccio.Height) / 2;

            botoAccio.Location = new Point(centreX, centreY);
            etiquetaEstat.Location = new Point(centreX, centreY - 30);

            botoAccio.Click += (s, ev) =>
            {
                etiquetaEstat.Text = "Hola Món des del codi!";
            };

            this.Controls.Add(etiquetaEstat);
            this.Controls.Add(botoAccio);
        }
    }
}