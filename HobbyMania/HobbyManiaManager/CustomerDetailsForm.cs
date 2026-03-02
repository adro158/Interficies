using System;
using System.Linq;
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
        private MoviesRepository moviesRepo;
        private RentalService rentalService;

        public CustomerDetailsForm(Customer customer)
        {
            InitializeComponent();
            currentCustomer = customer;
            rentalsRepo = RentalsRepository.GetInstance();
            customersRepo = CustomersRepository.Instance;
            moviesRepo = MoviesRepository.Instance;
            rentalService = new RentalService();

            this.Load += CustomerDetailsForm_Load;
            btnReturnMovie.Click += btnReturnMovie_Click;
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

        private void btnReturnMovie_Click(object sender, EventArgs e)
        {
            if (dgvActiveRentals.SelectedRows.Count > 0)
            {
                var selectedRental = (Rental)dgvActiveRentals.SelectedRows[0].DataBoundItem;
                var movieToReturn = moviesRepo.GetById(selectedRental.MovieId);

                rentalService.FinishRental(currentCustomer, movieToReturn, "Devuelto desde la interfaz");

                RefreshGrids();
                MessageBox.Show("Película devuelta correctamente.");
            }
            else
            {
                MessageBox.Show("Por favor, selecciona toda la fila de un alquiler activo haciendo clic en la flecha de la izquierda.");
            }
        }
    }
}