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
        public DbSet<Transaction> Transactions{ get; set; }
        public DbSet<Transfer> Transfers{ get; set; }
        public DbSet<Card> Cards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.Card)
                .WithOne(c => c.Account)
                .HasForeignKey<Card>(c => c.AccountId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
