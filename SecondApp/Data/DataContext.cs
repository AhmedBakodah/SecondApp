using Microsoft.EntityFrameworkCore;
using SecondApp.Models;

namespace SecondApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
            
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Client> Clients { get; set; }

        public DbSet<Banner> Banners { get; set; }




        public List<Event> GetEvents()
        {
            return Events.ToList();
        }


    }
}
