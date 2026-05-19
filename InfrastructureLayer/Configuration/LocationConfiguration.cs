using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
	public void Configure(EntityTypeBuilder<Location> builder)
	{
		builder.Property(l => l.Latitude).HasPrecision(9, 6);
		builder.Property(l => l.Longitude).HasPrecision(9, 6);
	}
}