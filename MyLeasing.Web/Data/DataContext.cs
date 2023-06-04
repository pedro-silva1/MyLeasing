using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data.Entities;

namespace MyLeasing.Web.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Entities.Owner> Owners { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
    }
}
