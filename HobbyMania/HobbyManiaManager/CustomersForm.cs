using System;
using System.Windows.Forms;
using HobbyManiaManager.Models;

namespace HobbyManiaManager
{
    public partial class CustomersForm : Form
    {
        private readonly CustomersRepository customersRepo;

        public CustomersForm()
        {
            InitializeComponent();
            customersRepo = CustomersRepository.Instance;

            this.Load += CustomersForm_Load;
            btnAddCustomer.Click += btnAddCustomer_Click;
            btnDetails.Click += btnDetails_Click;
        }

        private void CustomersForm_Load(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Por favor, rellena todos los campos.");
                return;
            }

            var newCustomer = new Customer(
                Customer.NextCustomerId,
                txtName.Text,
                txtEmail.Text,
                txtPhone.Text,
                DateTime.Now
            );

            try
            {
                customersRepo.Add(newCustomer);

                txtName.Clear();
                txtEmail.Clear();
                txtPhone.Clear();

                RefreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
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

        private void RefreshGrid()
        {
            dgvCustomers.DataSource = null;
            dgvCustomers.DataSource = customersRepo.GetAll();
        }
    }
}