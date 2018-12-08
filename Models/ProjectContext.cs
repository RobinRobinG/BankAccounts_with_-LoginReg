using Microsoft.EntityFrameworkCore;
 
namespace BankAccount.Models
{
    public class ProjectContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
