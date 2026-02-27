using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HobbyManiaManager.Models;

namespace HobbyManiaManager
{
    public partial class CustomersForm : Form
    {
        public CustomersForm()
        {
            InitializeComponent();
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count > 0)
            {
                var selectedCustomer = (Customer)dgvCustomers.SelectedRows[0].DataBoundItem;
                CustomerDetailsForm detailsForm = new CustomerDetailsForm(selectedCustomer);
                detailsForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Por favor, selecciona toda la fila de un cliente haciendo clic en la flecha de la izquierda.");
            }
        }
    }
}
