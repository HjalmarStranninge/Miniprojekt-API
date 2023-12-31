﻿using Microsoft.EntityFrameworkCore;
using Miniprojekt_API.Models;
using Miniprojekt_API.Models.DTO;
using Miniprojekt_API.Services;

namespace Miniprojekt_API
{
    // Class handling API interactions.
    public static class ApiHandler
    {      
        // Returns a list of all people in Json format.
        public static IResult ListPeople(IDbHelper dbHelper)
        {
            var people = dbHelper.GetAllPeople();
            return Results.Json(people);
        }

        // Returns a list of all of a persons interests in Json format.
        public static IResult ListPersonsInterests(IDbHelper dbHelper, int personId)
        {
            var interests = dbHelper.GetPersonsInterests(personId);

            return Results.Json(interests);
        }

        // Returns a list of all of a persons links in Json format.
        public static IResult ListPersonsLinks(IDbHelper dbHelper, int personId)
        {
            var links = dbHelper.GetPersonsLinks(personId);

            return Results.Json(links);
        }

        // Connects a person to an interest.
        public static IResult ConnectInterest(IDbHelper dbHelper, int personid, int interestId)
        {
            var result = dbHelper.ConnectInterest(personid, interestId);
            return result;
        }

        // Adds a new link and connects it to a person and an interest.
        public static IResult AddNewLink(IDbHelper dbHelper, int personid, int interestId, LinkDTO link)
        {
            var result = dbHelper.SaveLinkToDatabase(personid, interestId, link);
            return result;
        }
    }
}
