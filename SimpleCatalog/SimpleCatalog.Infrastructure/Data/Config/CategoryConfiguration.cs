using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleCatalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace SimpleCatalog.Infrastructure.Data.Config
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // Configure Category
           
            
                builder.HasKey(e => e.Id);
                builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
                builder.Property(e => e.Description).HasMaxLength(500);
            

            // Seed data
           builder.HasData(
                new Category { Id = 1, Name = "Electronics", Description = "Electronic devices", CreatedAt = DateTime.UtcNow },
                new Category { Id = 2, Name = "Books", Description = "Books and publications", CreatedAt = DateTime.UtcNow }
            );
        }
    }
}
