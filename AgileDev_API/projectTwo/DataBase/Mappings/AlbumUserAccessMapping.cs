using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using projectTwo.Models;


namespace projectTwo.DataBase.Mappings
{
	public class AlbumUserAccessEntityConfiguration : EntityCongurationMapper<AlbumUserAccess>
	{
		public override void Configure(EntityTypeBuilder<AlbumUserAccess> builder)
		{
			builder.HasKey(c => c.Id);

			builder.HasOne(s => s.User).WithMany(x => x.AlbumUserAccess)
				   .HasForeignKey(x => x.UserId)
				   .HasPrincipalKey(d => d.Id);

			builder.HasOne(s => s.Album).WithMany(x => x.AlbumUserAccess)
				   .HasForeignKey(x => x.AlbumId)
				   .HasPrincipalKey(d => d.Id);


			builder.ToTable("AlbumUserAccess");
		}
	}
}
