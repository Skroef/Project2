using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using projectTwo.Models;


namespace projectTwo.DataBase.Mappings
{
	public class ViewPostEntityConfiguration : EntityCongurationMapper<ViewPost>
	{
		public override void Configure(EntityTypeBuilder<ViewPost> builder)
		{
			builder.HasKey(c => c.Id);

			builder.HasOne(s => s.User).WithMany(x => x.ViewPost)
				   .HasForeignKey(x => x.UserId)
				   .HasPrincipalKey(d => d.Id);

			builder.HasOne(s => s.PostLog).WithMany(x => x.ViewPost)
				   .HasForeignKey(x => x.PostLogId)
				   .HasPrincipalKey(d => d.Id);

			builder.ToTable("PostLog");
		}
	}
}
