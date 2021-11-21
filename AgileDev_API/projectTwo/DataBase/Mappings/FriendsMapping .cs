using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using projectTwo.Models;


namespace projectTwo.DataBase.Mappings
{
	public class FriendsEntityConfiguration : EntityCongurationMapper<Friends>
	{
		public override void Configure(EntityTypeBuilder<Friends> builder)
		{
			builder.HasKey(c => c.Id);

			builder.HasOne(s => s.User).WithMany(x => x.Friends)
				   .HasForeignKey(x => x.UserId)
				   .HasPrincipalKey(d => d.Id);

			builder.ToTable("Friends");
		}
	}
}
