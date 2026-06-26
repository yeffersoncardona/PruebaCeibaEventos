using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.BuyerName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(r => r.BuyerEmail)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(r => r.Quantity)
                   .IsRequired();

            builder.Property(r => r.Status)
                   .IsRequired();

            builder.Property(r => r.ReservationCode)
                   .HasMaxLength(20);

            builder.Property(r => r.CreatedAt)
                   .IsRequired();

            builder.Property(r => r.CancelledAt)
                   .IsRequired(false);

            builder.HasOne<Event>()
                   .WithMany()
                   .HasForeignKey(r => r.EventId);
        }
    }
}
