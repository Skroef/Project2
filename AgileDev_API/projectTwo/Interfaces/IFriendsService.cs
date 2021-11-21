using System.Collections.Generic;
using projectTwo.DTOs;

namespace projectTwo.Interfaces
{
    public interface IFriendsService
	{
		void SendFriendRequest(SendFriendRequestDTO sendFriend);
		List<UserProfileDTO> GetgetFriendRequest(int logedinUserId);
		List<UserProfileDTO> GetSpecificFriend(int logedinUserId);
		void AcceptFriend(int id);
		List<FriendsDTO> GetFriend(int logedinUserId, bool following, bool followers);
		List<FriendsDTO> GetMyFriend(int logedinUserId);
	}
}
