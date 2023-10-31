using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Transactions;
using WiseMoneyTest.Entities;

namespace WiseMoneyTest.Data.DbConfiguration
{
    public class TransactionDbConfiguration : IEntityTypeConfiguration<Transactions>
    {
        public void Configure(EntityTypeBuilder<Transactions> builder)
        {
            builder
                .ToTable("Transactions");
            builder
                .HasKey(x => x.Id);
            builder
                .Property(x => x.AccountNumber)
                .HasColumnType("int");
            builder
                .Property(x => x.TransactionValue)
                .HasColumnType("money");
            builder
                .Property(x => x.Date)
                .HasColumnType("Datetime");
            builder
                .Property(x => x.TransactionType)
                .HasColumnType("varchar(10)")
                .HasMaxLength(10)
                .HasConversion<string>();
        }
    }
}
