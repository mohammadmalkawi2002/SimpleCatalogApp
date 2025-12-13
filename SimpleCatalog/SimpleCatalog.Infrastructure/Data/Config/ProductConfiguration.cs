using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleCatalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCatalog.Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p=>p.Name).IsRequired().HasMaxLength(200);
            builder.Property(p=>p.Description).HasMaxLength(1000);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");

            //Confugure Relationship:
            builder.HasOne(p=> p.Category)
                  .WithMany(c => c.Products)
                  .HasForeignKey(p => p.CategoryId)
                  .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
