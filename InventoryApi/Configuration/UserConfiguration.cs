using Microsoft.EntityFrameworkCore;
using InventoryApi.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Data.SqlClient;

namespace InventoryApi.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e  => e.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(p => p.PasswordHash)
                .IsRequired();

            builder.Property(r => r.Role)
                .IsRequired() 
                .HasMaxLength(50);

            builder.HasIndex(e => e.Email)
                .IsUnique();
        }
    }
}
