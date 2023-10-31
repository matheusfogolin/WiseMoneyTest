using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WiseMoneyTest.Entities;

namespace WiseMoneyTest.Data
{
    public class WiseMoneyTestDbContext : DbContext
    {
        public WiseMoneyTestDbContext(DbContextOptions<WiseMoneyTestDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Transactions> Transactions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
