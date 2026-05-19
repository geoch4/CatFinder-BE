using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AdvertisementConfiguration : IEntityTypeConfiguration<Advertisement>
{
	public void Configure(EntityTypeBuilder<Advertisement> builder)
	{
		builder.HasOne(a => a.Account)
			.WithMany(a => a.Advertisements)
			.HasForeignKey(a => a.AccountId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.HasOne(a => a.Cat)
			.WithMany(c => c.Advertisements)
			.HasForeignKey(a => a.CatId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(a => a.Location)
			.WithMany(l => l.Advertisements)
			.HasForeignKey(a => a.LocationId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}