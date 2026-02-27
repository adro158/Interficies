using System;
using System.Collections.Generic;
using System.Linq;
using HobbyManiaManager.Models;

namespace HobbyManiaManager.Repositories
{
    public class RentalsRepository
    {
        private static RentalsRepository Instance;
        List<Rental> Rentals;

        private RentalsRepository()
        {
            Rentals = new List<Rental>();
        }

        public static RentalsRepository GetInstance()
        {
            if (Instance == null)
            {
                Instance = new RentalsRepository();
            }
            return Instance;
        }

        public void Add(Rental rental)
        {
            Rentals.Add(rental);
        }

        public List<Rental> GetCustomerRentals(int customerId)
        {
            return Rentals
                .Where(r => r.CustomerId == customerId)
                .Select(r => (Rental)r.Clone())
                .ToList();
        }

        public List<Rental> GetAll()
        {
            return Rentals
                .Select(r => (Rental)r.Clone())
                .ToList();
        }

        public void Remove(Rental rental)
        {
            if (!Rentals.Remove(rental))
            {
                throw new InvalidOperationException("The specified rental does not exist in the list and could not be removed.");
            }
        }
    }
}
