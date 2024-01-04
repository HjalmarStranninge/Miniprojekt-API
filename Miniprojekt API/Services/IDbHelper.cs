using Microsoft.EntityFrameworkCore;
using Miniprojekt_API.Data;
using Miniprojekt_API.Models;
using Miniprojekt_API.Models.DTO;
using Miniprojekt_API.Models.ViewModels;
using System.Collections.Generic;
using System.Net;

namespace Miniprojekt_API.Services
{
    // Interface used for database interaction.
    public interface IDbHelper
    {
        List<ListAllPeopleViewModel> GetAllPeople();
        List<ListPersonsInterestsViewModel> GetPersonsInterests(int personId);
        List<ListPersonsLinksViewModel> GetPersonsLinks(int personId);
        public IResult ConnectInterest(int personId, int interestId);
        public IResult SaveLinkToDatabase(int personId, int interestId, LinkDTO link);
    }

    // Class implementing the IDbHelper interface.
    public class DbHelper : IDbHelper
    {
        private ProfileContext _context;
        public DbHelper(ProfileContext context)
        {
            _context = context;
        }


        // Retrieves a list of people from the database and maps them to a ViewModel for presentation.
        public List<ListAllPeopleViewModel> GetAllPeople()
        {
            List<ListAllPeopleViewModel> persons = _context.People.Select(p => new ListAllPeopleViewModel
            {
                FirstName = p.FirstName,
                LastName = p.LastName,
                PhoneNumber = p.PhoneNumber
            })
                .ToList();
            return persons;
        }


        // Returns a viewmodel list of all of a specific persons interests.
        public List<ListPersonsInterestsViewModel> GetPersonsInterests(int personId)
        {
            var person =
                _context.People
            .Where(p => p.Id == personId)
            .Include(p => p.Interests)
            .SingleOrDefault();

            List<ListPersonsInterestsViewModel> interests = person.Interests.Select(i => new ListPersonsInterestsViewModel
            {
                Title = i.Title,
                Description = i.Description
            })
                .ToList();
            return interests;
        }


        // Returns a viewmodel list of all of a specific persons links.
        public List<ListPersonsLinksViewModel> GetPersonsLinks(int personId)
        {
            var person =
                _context.People
            .Where(p => p.Id == personId)
            .Include(p => p.Links)
            .SingleOrDefault();

            List<ListPersonsLinksViewModel> links = person.Links.Select(l => new ListPersonsLinksViewModel
            {
                Url = l.Url
            })
                .ToList();
            return links;
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


        // Creates a new link and connects to an interest and a person.
        public IResult SaveLinkToDatabase(int personId, int interestId, LinkDTO link)
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

            person.Links.Add(new Link()
            {
                Url = link.Url
            });

            interest.Links.Add(new Link()
            {
                Url = link.Url
            });

            _context.SaveChanges();
            return Results.StatusCode((int)HttpStatusCode.Created);
        } 
    }
}
