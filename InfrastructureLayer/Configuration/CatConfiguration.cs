using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CatConfiguration : IEntityTypeConfiguration<Cat>
{
	public void Configure(EntityTypeBuilder<Cat> builder)
	{
		builder.HasOne(c => c.Account)
			.WithMany(a => a.Cats)
			.HasForeignKey(c => c.AccountId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}