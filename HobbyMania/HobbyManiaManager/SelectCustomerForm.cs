using System;
using System.Windows.Forms;
using HobbyManiaManager.Models;

namespace HobbyManiaManager
{
    public partial class SelectCustomerForm : Form
    {
        public Customer SelectedCustomer { get; private set; }
        public string Notes { get; private set; }

        public SelectCustomerForm()
        {
            InitializeComponent();
            // Cargamos los clientes en el desplegable
            cmbCustomers.DataSource = CustomersRepository.Instance.GetAll();
            cmbCustomers.DisplayMember = "Name";
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            SelectedCustomer = (Customer)cmbCustomers.SelectedItem;
            Notes = txtNotes.Text;

            if (SelectedCustomer == null)
            {
                MessageBox.Show("Por favor, selecciona un cliente.");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}