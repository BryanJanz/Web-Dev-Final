using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using _245Final.Models;

namespace _245Final.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("server=localhost;database=245Shop;user id=sa;password=P@ssword!;encrypt=false");
        }

        public DbSet<Games> Games { get; set; }
        public DbSet<Genres> Genres { get; set; }    
        public DbSet<Platform> Platform { get; set; }
        //public DbSet<Cart>? Cart { get; set; }
        //public DbSet<Cart> PreviousOrders { get; set; }   //This would be for purchase history
        
    }
}