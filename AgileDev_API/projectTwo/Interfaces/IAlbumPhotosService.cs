using System.Collections.Generic;
using projectTwo.DTOs;

namespace projectTwo.Interfaces
{
	public interface IAlbumPhotosService
	{
		List<AlbumPhotosDTO> GetPhotos(int AlbumId);
		void DeletePhoto(int photoId, int albumId);
		void AddPhoto(int photoId, int albumId);
	}
}
