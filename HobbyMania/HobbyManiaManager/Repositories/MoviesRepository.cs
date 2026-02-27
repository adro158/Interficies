using System.Collections.Generic;
using System.Linq;
using HobbyManiaManager.Models;

namespace HobbyManiaManager
{
    public class MoviesRepository
    {
        private static MoviesRepository instance;
        List<Movie> movies;

        private MoviesRepository() {
            movies = new List<Movie>();
        }

        public static MoviesRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MoviesRepository();
                }
                return instance;
            }
        }

        public Movie GetById(int id) => (Movie)(movies.Find(m => m.Id == id)?.Clone());

        public void AddAll(List<Movie> movies) { 
            this.movies.AddRange(movies.Select(m => (Movie) m.Clone()));
        }

        public int Count => movies.Count;
    }
}
