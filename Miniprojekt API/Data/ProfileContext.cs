using Microsoft.EntityFrameworkCore;
using Miniprojekt_API.Models;

namespace Miniprojekt_API.Data
{
    public class ProfileContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Link> Links { get; set; }
        public ProfileContext(DbContextOptions<ProfileContext> options) : base(options) {}
    }
}
