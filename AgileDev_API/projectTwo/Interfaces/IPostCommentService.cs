using System.Collections.Generic;
using projectTwo.DTOs;

namespace projectTwo.Interfaces
{
	public interface IPostCommentService
	{
		//UserInfoDTO getAllUsersInfo();
		//void registerUser(RegisterUserDTO registerUser);

		//UserProfileDTO GetCurrentUser(string email);
		//void editUser(UserProfileDTO pack);
		//UserProfileDTO GetUserProfileImg(string email);
		//void CreatePost(PostLogDTO pack);
		//List<PostLogDTO> GetUserLatestPosts(int logedinUserId);

		public void AddComment(PostCommentDTO comment);
		List<PostCommentDTO> GetComment(int logedinUserId);
	}
}
