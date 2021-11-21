using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using projectTwo.Models;


namespace projectTwo.DataBase.Mappings
{
	public class UserEntityConfiguration : EntityCongurationMapper<User>
	{
		public override void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasKey(c => c.Id);

			//builder.HasOne(s => s.Status).WithMany(x => x.Users)
			//	   .HasForeignKey(x => x.StatusId)
			//	   .HasPrincipalKey(d => d.Id);

			builder.HasOne(s => s.UserRole).WithMany(x => x.Users)
				   .HasForeignKey(x => x.UserRoleId)
				   .HasPrincipalKey(d => d.Id);

			builder.ToTable("User");
		}
	}
}
