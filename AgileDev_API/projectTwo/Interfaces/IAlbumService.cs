using System.Collections.Generic;
using projectTwo.DTOs;

namespace projectTwo.Interfaces
{
	public interface IAlbumService
	{
		void CreateAlbum(AlbumDTO album);
		List<AlbumDTO> GetAlbums(int logedinUserId);
		void DeleteAlbum(int albumId);
		List<AlbumDTO> GetAlbumsAsAFriend(int logedinUserId);
	}
}
