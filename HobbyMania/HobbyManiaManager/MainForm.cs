using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using HobbyManiaManager.Models;

namespace HobbyManiaManager
{
    public partial class MainForm : Form
    {
        private readonly MoviesRepository moviesRepository;

        public MainForm()
        {
            InitializeComponent();
            moviesRepository = MoviesRepository.Instance;

            // Conectamos los botones manualmente para que no fallen
            btnMovies.Click += btnMovies_Click;
            btnCustomers.Click += btnCustomers_Click;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadMovies();
            labelMoviesCounter.Text = $"{moviesRepository.Count} movies loaded.";
        }

        private void LoadMovies()
        {
            string filePath = Path.Combine(Application.StartupPath, "Resources", "tmdb_top_movies_small.json");
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                List<Movie> movies = JsonConvert.DeserializeObject<List<Movie>>(json);
                moviesRepository.AddAll(movies);
            }
        }

        private void btnMovies_Click(object sender, EventArgs e)
        {
            MoviesForm f = new MoviesForm();
            f.ShowDialog();
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            CustomersForm f = new CustomersForm();
            f.ShowDialog();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            // Usamos el namespace HobbyManiaManager.Help si es necesario
            HobbyManiaManager.Help.HelpForm f = new HobbyManiaManager.Help.HelpForm();
            f.ShowDialog();
        }
    }
}