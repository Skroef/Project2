using System.Collections.Generic;
using projectTwo.DTOs;

namespace projectTwo.Interfaces
{
    public interface IPostLogService
	{
		//UserInfoDTO getAllUsersInfo();
		//void registerUser(RegisterUserDTO registerUser);
		
		//UserProfileDTO GetCurrentUser(string email);
		//void editUser(UserProfileDTO pack);
		//UserProfileDTO GetUserProfileImg(string email);
		void CreatePost(PostLogDTO pack);
		List<PostLogDTO> GetUserLatestPosts(int logedinUserId);
		List<PostLogDTO> GetPostssharedWithMe(int logedinUserId);
		List<PostLogDTO> GetUserPostLog(int logedinUserId);
		void UpdatePost(PostLogDTO updateData);
		void DeletePost(int postId);
	}
}
