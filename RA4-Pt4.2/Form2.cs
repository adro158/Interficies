using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using RA4_Pt4._2;

namespace RA4_Pt4._2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            ActualitzarGrid();
        }

        private void ActualitzarGrid()
        {
            dgvUsuaris.DataSource = null;
            dgvUsuaris.DataSource = GestorDades.Instancia.LlistaUsuaris.Select(u => new {
                u.Id,
                u.Nom,
                u.Correu,
                Tipus = u is UsuariEstudiant ? "Estudiant" : "Admin"
            }).ToList();
        }

        private void btnNou_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.ShowDialog();
            ActualitzarGrid();
        }

        private void btnEditor_Click(object sender, EventArgs e)
        {
            if (dgvUsuaris.CurrentRow != null)
            {
                var usuari = GestorDades.Instancia.LlistaUsuaris[dgvUsuaris.CurrentRow.Index];
                Form3 f3 = new Form3(usuari);
                f3.ShowDialog();
                ActualitzarGrid();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvUsuaris.CurrentRow != null)
            {
                var usuari = GestorDades.Instancia.LlistaUsuaris[dgvUsuaris.CurrentRow.Index];
                if (MessageBox.Show($"Segur que vols eliminar a {usuari.Nom}?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    GestorDades.Instancia.LlistaUsuaris.Remove(usuari);
                    ActualitzarGrid();
                }
            }
        }
    }
}