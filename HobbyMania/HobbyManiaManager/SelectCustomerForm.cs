using System;
using System.Windows.Forms;
using HobbyManiaManager.Models;

namespace HobbyManiaManager
{
    public partial class SelectCustomerForm : Form
    {
        public Customer SelectedCustomer { get; private set; }
        public string Notes => txtNotes.Text;

        public SelectCustomerForm()
        {
            InitializeComponent();
            cmbCustomers.DataSource = CustomersRepository.Instance.GetAll();
            cmbCustomers.DisplayMember = "Name";
            btnConfirm.Click += (s, e) => {
                SelectedCustomer = (Customer)cmbCustomers.SelectedItem;
                this.DialogResult = DialogResult.OK;
            };
        }
    }
}