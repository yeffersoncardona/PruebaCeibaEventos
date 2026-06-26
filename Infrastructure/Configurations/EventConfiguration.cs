using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(e => e.Type)
                   .IsRequired();

            builder.Property(e => e.Capacity)
                   .IsRequired();

            builder.Property(e => e.Price)
                   .HasColumnType("decimal(18,2)");

            builder.HasOne<Venue>()
                   .WithMany()
                   .HasForeignKey(e => e.VenueId);
        }
    }

}
