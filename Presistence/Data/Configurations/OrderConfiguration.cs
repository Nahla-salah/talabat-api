using Domain.Models.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.Property(o =>o.Subtotal).HasColumnType("decimal(8,2)");

            builder.HasMany(p => p.OrderItems)
                .WithOne();

            builder.HasOne(o =>o.DeliveryMethod)
                .WithMany()
                .HasForeignKey(o => o.DeliveryMethodId);

            builder.OwnsOne(o => o.ShippingAddress ,sh =>sh.WithOwner());

        }
    }
}
