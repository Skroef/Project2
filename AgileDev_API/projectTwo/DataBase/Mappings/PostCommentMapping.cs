using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using projectTwo.Models;


namespace projectTwo.DataBase.Mappings
{
	public class PostCommentEntityConfiguration : EntityCongurationMapper<PostComment>
	{
		public override void Configure(EntityTypeBuilder<PostComment> builder)
		{
			builder.HasKey(c => c.Id);

			builder.HasOne(s => s.User).WithMany(x => x.PostComment)
				   .HasForeignKey(x => x.UserId)
				   .HasPrincipalKey(d => d.Id);

			builder.HasOne(s => s.PostLog).WithMany(x => x.PostComment)
				   .HasForeignKey(x => x.PostLogId)
				   .HasPrincipalKey(d => d.Id);

			builder.ToTable("PostComment");
		}
	}
}
