using System;
using System.Drawing;
using System.Windows.Forms;

namespace ElMissatger
{
    public partial class Form1 : Form
    {
        private TextBox txtNom;
        private Button btnEnviar;

        public Form1()
        {
            InitializeComponent();

            txtNom = new TextBox();
            txtNom.Location = new Point(50, 50);
            txtNom.Width = 200;

            btnEnviar = new Button();
            btnEnviar.Text = "Enviar";
            btnEnviar.Location = new Point(50, 90);
            btnEnviar.Click += BtnEnviar_Click;

            this.Controls.Add(txtNom);
            this.Controls.Add(btnEnviar);
        }

        private void BtnEnviar_Click(object sender, EventArgs e)
        {
            string nom = txtNom.Text;
            Form2 f2 = new Form2(nom);
            f2.ShowDialog();
        }
    }
}