using Microsoft.EntityFrameworkCore;
using Miniprojekt_API.Data;
using Miniprojekt_API.Models;
using Miniprojekt_API.Models.DTO;
using Miniprojekt_API.Models.ViewModels;
using System.Collections.Generic;
using System.Net;

namespace Miniprojekt_API.Services
{
    public interface IDbHelper
    {
        List<ListAllPersonsViewModel> GetAllPeople();
        public IResult ConnectInterest(int personId, int interestId);
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
        public List<ListAllPersonsViewModel> GetAllPeople()
        {
            List<ListAllPersonsViewModel> persons = _context.People.Select(p => new ListAllPersonsViewModel 
            { 
                FirstName = p.FirstName, 
                LastName = p.LastName,
                PhoneNumber = p.PhoneNumber
                })
                .ToList();
            return persons;
        }

        // Adds new interest to database and links it to a person.
        public IResult ConnectInterest(int personId, int interestId)
        {            
            var interest =
                _context.Interests
            .Where(i => i.Id == interestId)
            .SingleOrDefault();

            if (!Utility.DoesEntityExist(interest))
            {
                return Results.BadRequest($"No {nameof(interest)} with ID {interestId} exists.");
            }

            var person =
                _context.People
            .Where(p => p.Id == personId)
            .Include(p => p.Interests)
            .SingleOrDefault();

            if (!Utility.DoesEntityExist(person))
            {
                return Results.BadRequest($"No {nameof(person)} with ID {personId} exists.");
            }

            if (person.Interests.Contains(interest))
            {
                return Results.Conflict($"{person.FirstName} {person.LastName} has already got the interest {interest.Title}");
            }

            person.Interests
                .Add(interest);

            _context.SaveChanges();

            return Results.StatusCode((int)HttpStatusCode.Created);
        }
    }
}
