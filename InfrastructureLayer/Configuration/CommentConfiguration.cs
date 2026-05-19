using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
	public void Configure(EntityTypeBuilder<Comment> builder)
	{
		builder.HasOne(c => c.Advertisement)
			.WithMany(a => a.Comments)
			.HasForeignKey(c => c.AdvertisementId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.HasOne(c => c.Account)
			.WithMany(a => a.Comments)
			.HasForeignKey(c => c.AccountId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasOne<Comment>()
			.WithMany()
			.HasForeignKey(c => c.ParentCommentId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}