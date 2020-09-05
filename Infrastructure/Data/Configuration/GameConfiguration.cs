using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configuration
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.Property(g => g.Id).IsRequired();
            builder.Property(g => g.Title).IsRequired().HasMaxLength(100);
            builder.Property(g => g.Description).IsRequired().HasMaxLength(300);
            builder.Property(g => g.Price).HasColumnType("decimal(18,2)");
            builder.Property(g => g.PictureUrl).IsRequired();
            builder.HasOne(g => g.Company).WithMany()
                .HasForeignKey(g => g.CompanyId);
            builder.HasOne(g => g.Genre).WithMany()
                .HasForeignKey(g => g.GenreId);
        }
    }
}
