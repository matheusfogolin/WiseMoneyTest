using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WiseMoneyTest.Entities;

namespace WiseMoneyTest.Data.DbConfiguration
{
    public class AccountDbConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder
                .ToTable("Accounts");
            builder
                .HasKey(x => x.Id);
            builder
                .Property(x => x.AccountNumber)
                .HasColumnType("int");
            builder
                .Property(x => x.Balance)
                .HasColumnType("money");          
        }
    }
}
