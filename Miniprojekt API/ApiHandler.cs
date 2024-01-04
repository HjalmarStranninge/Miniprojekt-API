using Microsoft.EntityFrameworkCore;
using Miniprojekt_API.Models;
using Miniprojekt_API.Models.DTO;
using Miniprojekt_API.Services;

namespace Miniprojekt_API
{
    public static class ApiHandler
    {      
        // Returns a list of all people in Json format.
        public static IResult ListPeople(IDbHelper dbHelper)
        {
            var people = dbHelper.GetAllPeople();
            return Results.Json(people);
        }

        public static IResult ConnectInterest(IDbHelper dbHelper, int personid, int interestId)
        {
            var result = dbHelper.ConnectInterest(personid, interestId);
            return result;
        }
    }
}
