using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AdvertisementImageConfiguration : IEntityTypeConfiguration<AdvertisementImage>
{
	public void Configure(EntityTypeBuilder<AdvertisementImage> builder)
	{
		builder.HasOne(i => i.Advertisement)
			.WithMany(a => a.AdvertisementImages)
			.HasForeignKey(i => i.AdvertisementId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}