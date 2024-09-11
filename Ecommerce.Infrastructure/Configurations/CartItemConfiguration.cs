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
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.IsDeleted)
                .HasDefaultValue(false);

            builder.HasQueryFilter(c => !c.IsDeleted);

            builder.Property(b => b.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(b => b.UpdatedDate)
                .IsRequired(false);

            builder.Property(c => c.ProductId)
                .IsRequired();

            builder.Property(c => c.CartId)
                .IsRequired();

            builder.Property(c => c.Quantity)
                .IsRequired();

            builder.HasOne(c => c.Product)
                .WithMany()
                .HasForeignKey(c => c.ProductId);


        }
    }
}
