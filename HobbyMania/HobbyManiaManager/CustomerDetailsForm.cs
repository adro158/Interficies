using System;
using System.Windows.Forms;
using HobbyManiaManager.Models;
using HobbyManiaManager.Repositories;

namespace HobbyManiaManager
{
    public partial class CustomerDetailsForm : Form
    {
        private Customer currentCustomer;
        private RentalsRepository rentalsRepo;
        private CustomersRepository customersRepo;
        private RentalService rentalService;

        public CustomerDetailsForm(Customer customer)
        {
            InitializeComponent();
            currentCustomer = customer;
            rentalsRepo = RentalsRepository.GetInstance();
            customersRepo = CustomersRepository.Instance;
            rentalService = new RentalService();
        }

        private void CustomerDetailsForm_Load(object sender, EventArgs e)
        {
            lblName.Text = currentCustomer.Name;
            lblEmail.Text = currentCustomer.Email;
            RefreshGrids();
        }

        private void RefreshGrids()
        {
            dgvActiveRentals.DataSource = null;
            dgvActiveRentals.DataSource = rentalsRepo.GetCustomerRentals(currentCustomer.Id);

            var updatedCustomer = customersRepo.GetById(currentCustomer.Id);
            dgvHistory.DataSource = null;
            dgvHistory.DataSource = updatedCustomer.RentalsHistory;
        }
    }
}