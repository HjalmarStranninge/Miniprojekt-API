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

            app.MapGet("/people", ApiHandler.ListPeople);
            app.MapPost("/people/{personId}/interest/{interestId}/connect", ApiHandler.ConnectInterest);

            app.Run();
        }
    }
}
