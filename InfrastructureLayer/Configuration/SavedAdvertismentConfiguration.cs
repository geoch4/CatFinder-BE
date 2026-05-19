using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SavedAdvertisementConfiguration : IEntityTypeConfiguration<SavedAdvertisement>
{
	public void Configure(EntityTypeBuilder<SavedAdvertisement> builder)
	{
		builder.HasOne(s => s.Account)
			.WithMany(a => a.SavedAdvertisements)
			.HasForeignKey(s => s.AccountId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.HasOne(s => s.Advertisement)
			.WithMany(a => a.SavedAdvertisements)
			.HasForeignKey(s => s.AdvertisementId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasIndex(s => new { s.AccountId, s.AdvertisementId }).IsUnique();
	}
}