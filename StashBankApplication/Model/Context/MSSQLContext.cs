using Microsoft.EntityFrameworkCore;

namespace StashBankApplication.Model.Context
{
    public class MSSQLContext : DbContext
    {
        public MSSQLContext(DbContextOptions <MSSQLContext> options) : base(options)
        {
        }
    
        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
    }

}
