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
        private readonly RentalService service;
        private readonly MoviesRepository moviesRepository;
        private readonly CustomersRepository customersRepository;

        public MainForm()
        {
            InitializeComponent();
            service = new RentalService();
            moviesRepository = MoviesRepository.Instance;
            customersRepository = CustomersRepository.Instance;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadMovies();
            Demo();
            labelMoviesCounter.Text = $"{moviesRepository.Count} movies loaded.";
        }

        private void LoadMovies()
        {
            string filePath = "Resources/tmdb_top_movies_small.json";
            string json = File.ReadAllText(filePath);
            List<Movie> movies = JsonConvert.DeserializeObject<List<Movie>>(json);
            moviesRepository.AddAll(movies);
        }

        private void Demo()
        {
            // Code to demonstrate the call flow for creating a customer and creating/finishing a rental.
            var c = new Customer(Customer.NextCustomerId, "Nacho", "nacho@xtec.cat", "618 477 246", DateTime.Now);
            customersRepository.Add(c);
            
            var c2 = new Customer(Customer.NextCustomerId, "Paco", "paco@xtec.cat", "618 477 666", DateTime.Now);
            customersRepository.Add(c2);


            var m = moviesRepository.GetById(238);
            service.Rent(c, m, "El cliente avisa que el dvd está muy rayado");
            service.FinishRental(c, m, "El cliente avisa que el dvd está muy rayado. " +
                "El cliente confirma que no funciona por lo rayado que está");

            var m2 = moviesRepository.GetById(240);
            service.Rent(c, m2);
            service.FinishRental(c, m2, "El cliente deja a deber 200ptas de este alquiler");
        }
    }
}
