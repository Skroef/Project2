using System.Collections.Generic;
using projectTwo.DTOs;

namespace projectTwo.Interfaces
{
    public interface IViewPostService
	{
		//UserInfoDTO getAllUsersInfo();
		//void registerUser(RegisterUserDTO registerUser);

		//UserProfileDTO GetCurrentUser(string email);
		//void editUser(UserProfileDTO pack);
		//UserProfileDTO GetUserProfileImg(string email);
		//void CreatePost(PostLogDTO pack);
		//List<PostLogDTO> GetUserLatestPosts(int logedinUserId);

		void AddSpesificUsersToPost(ViewPostDTO specificPostDetails);
		List<ViewPostDTO> GetUser(int logedinUserId, int postId);
		void DeleteFriendAccess(int photoId, int friendUserId);
		void AddFriendToPhoto(int photoId, int friendId);
	}
}
