using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HobbyManiaManager
{
    public partial class MoviesForm : Form
    {
        private readonly MoviesRepository moviesRepo;

        public MoviesForm()
        {
            InitializeComponent();
            moviesRepo = MoviesRepository.Instance;
        }

        private void MoviesForm_Load(object sender, EventArgs e)
        {
            dgvMovies.DataSource = moviesRepo.GetAll();
        }

        private void dgvMovies_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}