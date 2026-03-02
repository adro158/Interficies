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
                MessageBox.Show("Tots els camps (Nom, Email, Telèfon) són obligatoris.", "Error de validació", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newCustomer = new Customer(
                Customer.NextCustomerId,
                txtName.Text,
                txtEmail.Text,
                txtPhone.Text,
                DateTime.Now
            );

            CustomersRepository.Instance.Add(newCustomer);

            txtName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();

            dgvCustomers.DataSource = null;
            dgvCustomers.DataSource = CustomersRepository.Instance.GetAll();

            MessageBox.Show("Client afegit amb èxit.");
        


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

        private void dgvCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
        }
    }
}