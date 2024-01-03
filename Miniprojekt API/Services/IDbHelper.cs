using Microsoft.EntityFrameworkCore;
using Miniprojekt_API.Data;
using Miniprojekt_API.Models;
using Miniprojekt_API.Models.ViewModels;
using System.Collections.Generic;

namespace Miniprojekt_API.Services
{
    public interface IDbHelper
    {
        List<ListAllPersonsViewModel> GetAllPersons();
    }

    // Class implementing the IDbHelper interface.
    public class DbHelper : IDbHelper
    {
        private ProfileContext _context;
        public DbHelper(ProfileContext context)
        {
            _context = context;
        }
        // Retrieves a list of persons from the database and maps them to a ViewModel for presentation.
        public List<ListAllPersonsViewModel> GetAllPersons()
        {
            List<ListAllPersonsViewModel> persons = _context.Persons.Select(p => new ListAllPersonsViewModel 
            { 
                FirstName = p.FirstName, 
                LastName = p.LastName,
                PhoneNumber = p.PhoneNumber
                })
                .ToList();
            return persons;
        }
    }
}
