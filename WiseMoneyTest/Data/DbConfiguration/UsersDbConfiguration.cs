using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WiseMoneyTest.Entities;

namespace WiseMoneyTest.Data.DbConfiguration
{
    public class UsersDbConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .ToTable("Users");
            builder
                .HasKey(x => x.Id);
            builder
                .Property(x => x.EmailAdress)
                .HasMaxLength(50)
                .HasColumnType("varchar(50)");
            builder
                .Property(x => x.Password)
                .HasMaxLength(50)
                .HasColumnType("varchar(50)");
            builder
                .HasMany(x => x.Accounts)
                .WithOne()
                .HasForeignKey(x => x.UserId);
        }
    }
}
