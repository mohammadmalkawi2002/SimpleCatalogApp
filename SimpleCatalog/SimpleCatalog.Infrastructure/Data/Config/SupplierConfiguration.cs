using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleCatalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCatalog.Infrastructure.Data.Config
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s=>s.Name).HasMaxLength(100).IsRequired();
            builder.Property(s => s.Email).HasMaxLength(200);
            builder.HasIndex(s => s.Email).IsUnique();


            builder.Property(e => e.Phone).HasMaxLength(50);

            //Config Supplier => Product Relationship:
            builder.HasMany(S => S.Products)
                .WithOne(P => P.Supplier)
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(

                 new Supplier
                 {
                     Id = 1,
                     Name = "Electro Supplies",
                     Email = "contact@electrosupplies.com",
                     Phone = "123-456-7890",
                     CreatedAt = DateTime.UtcNow
                 },
    new Supplier
    {
        Id = 2,
        Name = "Book World",
        Email = "info@bookworld.com",
        Phone = "987-654-3210",
        CreatedAt = DateTime.UtcNow
    }
                );
        }
    }
}
