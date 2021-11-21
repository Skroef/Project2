using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using projectTwo.Models;


namespace projectTwo.DataBase.Mappings
{
	public class AlbumPhotoEntityConfiguration : EntityCongurationMapper<AlbumPhotos>
	{
		public override void Configure(EntityTypeBuilder<AlbumPhotos> builder)
		{
			builder.HasKey(c => c.Id);


			builder.HasOne(s => s.Album).WithMany(x => x.AlbumPhotos);

			builder.HasOne(s => s.PostLog).WithMany(x => x.AlbumPhotos);

			builder.ToTable("AlbumPhotos");
		}
	}
}
