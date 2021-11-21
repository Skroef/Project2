using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using projectTwo.Models;


namespace projectTwo.DataBase.Mappings
{
	public class AlbumEntityConfiguration : EntityCongurationMapper<Album>
	{
		public override void Configure(EntityTypeBuilder<Album> builder)
		{
			builder.HasKey(c => c.Id);

			builder.HasOne(s => s.User).WithMany(x => x.Album)
				   .HasForeignKey(x => x.UserId)
				   .HasPrincipalKey(d => d.Id);

			//builder.HasOne(s => s.PostLog).WithMany(x => x.Album)
			//	   .HasForeignKey(x => x.PostLogId)
			//	   .HasPrincipalKey(d => d.Id);


			builder.ToTable("Album");
		}
	}
}
