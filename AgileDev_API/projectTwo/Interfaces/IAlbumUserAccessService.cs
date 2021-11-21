using System.Collections.Generic;
using projectTwo.DTOs;

namespace projectTwo.Interfaces
{
	public interface IAlbumUserAccessService
	{
		void AddFriendToAlbum(int albumId, int friendId);
	}
}
