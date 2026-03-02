using System;
using System.Data;
using System.Linq;
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

        private void btnFilter_Click(object sender, EventArgs e)
        {
            string searchText = txtSearchTitle.Text.ToLower();
            double minScore = (double)nudMinScore.Value;

            var filteredMovies = moviesRepo.GetAll()
                .Where(m => (string.IsNullOrEmpty(searchText) || m.Title.ToLower().Contains(searchText)) &&
                            m.VoteAverage >= minScore)
                .ToList();

            dgvMovies.DataSource = filteredMovies;
        }

        private void dgvMovies_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}