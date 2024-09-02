using Ecommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Configurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("Brands");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(b => b.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(b => b.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(b => b.UpdatedDate)
                .IsRequired(false);

            builder.HasMany(b => b.Products)
                .WithOne(b => b.Brand)
                .HasForeignKey(b => b.BrandId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
