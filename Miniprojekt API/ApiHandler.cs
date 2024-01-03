using Miniprojekt_API.Services;

namespace Miniprojekt_API
{
    public class ApiHandler
    {
        // Returns a list of all people in Json format.
        public static IResult ListPersons(IDbHelper dbHelper)
        {
            var persons = dbHelper.GetAllPersons();
            return Results.Json(persons);
        }
    }
}
