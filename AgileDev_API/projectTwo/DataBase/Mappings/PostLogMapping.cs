using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using projectTwo.Models;


namespace projectTwo.DataBase.Mappings
{
	public class PostLogEntityConfiguration : EntityCongurationMapper<PostLog>
	{
		public override void Configure(EntityTypeBuilder<PostLog> builder)
		{
			builder.HasKey(c => c.Id);

			builder.HasOne(s => s.User).WithMany(x => x.PostLogs)
				   .HasForeignKey(x => x.UserId)
				   .HasPrincipalKey(d => d.Id);

			builder.ToTable("PostLog");
		}
	}
}
