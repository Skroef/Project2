using System.Collections.Generic;
using projectTwo.DTOs;

namespace projectTwo.Interfaces
{
    public interface IUserService
	{
		UserInfoDTO getAllUsersInfo();
		void registerUser(RegisterUserDTO registerUser);
		AuthInfoDTO getUserInfo(string email);
		UserProfileDTO GetCurrentUser(int id);
		void editUser(UserProfileDTO pack);
		UserProfileDTO GetUserProfileImg(int logedinUserId);
		UserProfileDTO GetUserProfile(int logedinUserId);
		UserProfileDTO FindFriend(string email);
		//List<UserProfileDTO> GetgetFriendRequest();

	}
}
