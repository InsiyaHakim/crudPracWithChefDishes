using Microsoft.EntityFrameworkCore;
 
namespace CRUDpractice.Models
{
    public class MyContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public MyContext(DbContextOptions options) : base(options) { }

        public DbSet<User> user {get;set;}
        public DbSet<Chef> Chef {get;set;}
        public DbSet<Dishes> Dishes{get;set;}
    }
}