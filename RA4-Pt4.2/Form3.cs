using System;
using System.Windows.Forms;

namespace RA4_Pt4._2
{
    public partial class Form3 : Form
    {
        private UsuariBase? _usuariEdicio;

        public Form3()
        {
            InitializeComponent();
            cmbTipus.SelectedItem = "Estudiant";
        }

        public Form3(UsuariBase usuari) : this()
        {
            _usuariEdicio = usuari;
            txtNom.Text = usuari.Nom;
            txtCorreu.Text = usuari.Correu;

            if (usuari is UsuariEstudiant e)
            {
                cmbTipus.SelectedItem = "Estudiant";
                txtExtra.Text = e.NotaMitjana.ToString();
            }
            else if (usuari is UsuariAdministrador a)
            {
                cmbTipus.SelectedItem = "Admin";
                txtExtra.Text = a.Departament;
            }
        }

        private void cmbTipus_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblExtra.Text = cmbTipus.Text == "Estudiant" ? "Nota Mitjana:" : "Departament:";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNom.Text))
            {
                MessageBox.Show("El nom és obligatori.", "Avís", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtExtra.Text))
            {
                MessageBox.Show("El camp extra no pot estar buit.", "Avís", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_usuariEdicio == null)
            {
                UsuariBase nou;
                if (cmbTipus.Text == "Estudiant")
                {
                    if (!double.TryParse(txtExtra.Text, out double nota))
                    {
                        MessageBox.Show("La nota ha de ser un número vàlid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    nou = new UsuariEstudiant { Id = GestorDades.Instancia.LlistaUsuaris.Count + 1, Nom = txtNom.Text, Correu = txtCorreu.Text, NotaMitjana = nota };
                }
                else
                {
                    nou = new UsuariAdministrador { Id = GestorDades.Instancia.LlistaUsuaris.Count + 1, Nom = txtNom.Text, Correu = txtCorreu.Text, Departament = txtExtra.Text };
                }
                GestorDades.Instancia.LlistaUsuaris.Add(nou);
            }
            else
            {
                _usuariEdicio.Nom = txtNom.Text;
                _usuariEdicio.Correu = txtCorreu.Text;

                if (_usuariEdicio is UsuariEstudiant es)
                {
                    if (!double.TryParse(txtExtra.Text, out double nota))
                    {
                        MessageBox.Show("La nota ha de ser un número vàlid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    es.NotaMitjana = nota;
                }
                else if (_usuariEdicio is UsuariAdministrador ad)
                {
                    ad.Departament = txtExtra.Text;
                }
            }
            this.Close();
        }

        private void txtNom_TextChanged(object sender, EventArgs e) { }
        private void txtCorreu_TextChanged(object sender, EventArgs e) { }
        private void lblExtra_Click(object sender, EventArgs e) { }
        private void txtExtra_TextChanged(object sender, EventArgs e) { }
    }
}