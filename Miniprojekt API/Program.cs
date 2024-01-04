using Microsoft.EntityFrameworkCore;
using Miniprojekt_API.Data;
using Miniprojekt_API.Services;

namespace Miniprojekt_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Creating context and scoping IDbHelper to the correct class.
            string connectionString = builder.Configuration.GetConnectionString("ProfileContext");
            builder.Services.AddDbContext<ProfileContext>(opt => opt.UseSqlServer(connectionString));
            builder.Services.AddScoped<IDbHelper, DbHelper>();

            var app = builder.Build();

            // GET-endpoints
            app.MapGet("/people", ApiHandler.ListPeople);
            app.MapGet("/people/{personId}/interests", ApiHandler.ListPersonsInterests);
            app.MapGet("/people/{personId}/links", ApiHandler.ListPersonsLinks);

            // POST-endpoints
            app.MapPost("/people/{personId}/interests/{interestId}/connect", ApiHandler.ConnectInterest);
            app.MapPost("/people/{personId}/interests/{interestId}/addlink", ApiHandler.AddNewLink);

            app.Run();
        }
    }
}
