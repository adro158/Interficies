using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LlistaCompra
{
    public class Producte
    {
        public string Nom { get; set; }
        public double Preu { get; set; }
    }

    public partial class Form1 : Form
    {
        private DataGridView dgvProductes;
        private Button btnEliminar;
        private List<Producte> laMevaLlista;

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(400, 350);

            dgvProductes = new DataGridView();
            dgvProductes.Location = new Point(20, 20);
            dgvProductes.Size = new Size(340, 200);
            dgvProductes.AllowUserToAddRows = false;
            dgvProductes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductes.MultiSelect = false;

            dgvProductes.Columns.Add("NomCol", "Nom");
            dgvProductes.Columns.Add("PreuCol", "Preu (€)");

            btnEliminar = new Button();
            btnEliminar.Text = "Eliminar Seleccionat";
            btnEliminar.Location = new Point(20, 240);
            btnEliminar.Size = new Size(150, 40);
            btnEliminar.Click += BtnEliminar_Click;

            this.Controls.Add(dgvProductes);
            this.Controls.Add(btnEliminar);

            laMevaLlista = new List<Producte>
            {
                new Producte { Nom = "Pomes", Preu = 2.50 },
                new Producte { Nom = "Llet", Preu = 1.15 },
                new Producte { Nom = "Ous", Preu = 3.20 }
            };

            foreach (Producte p in laMevaLlista)
            {
                dgvProductes.Rows.Add(p.Nom, p.Preu);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductes.SelectedRows.Count > 0)
            {
                DialogResult resposta = MessageBox.Show(
                    "Estàs segur?",
                    "Alerta",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (resposta == DialogResult.Yes)
                {
                    dgvProductes.Rows.RemoveAt(dgvProductes.SelectedRows[0].Index);
                }
            }
        }
    }
}