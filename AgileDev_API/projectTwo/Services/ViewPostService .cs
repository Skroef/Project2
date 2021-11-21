using projectTwo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using projectTwo.DTOs;
using projectTwo.Database;
using projectTwo.Models;
using Microsoft.EntityFrameworkCore;

namespace projectTwo.Services
{
    public class ViewPostService : IViewPostService
    {
        private readonly ProjectTwoContext _projectTwoContext;
        private readonly IProjectTwoUnitOfWork _projectTwoUnitOfWork;

		public ViewPostService(ProjectTwoContext projectTwoContext, IProjectTwoUnitOfWork projectTwoUnitOfWork)
        {
            _projectTwoContext = projectTwoContext;
            _projectTwoUnitOfWork = projectTwoUnitOfWork;
        }


		public void AddSpesificUsersToPost(ViewPostDTO specificPostDetails)
		{
			var createPost = new PostLog
			{
				UserId = specificPostDetails.ParentUserId,
				ImageUrl = specificPostDetails.ImageUrl,
				Caption = specificPostDetails.Caption,
				IsActive = true,
				CreatedOn = DateTime.Now,
			};

			_projectTwoUnitOfWork.PostLog.Add(createPost);

			_projectTwoUnitOfWork.Save();

			
			var addUser = new ViewPost { };

			foreach (var i in specificPostDetails.UserId) 
			{
				addUser = new ViewPost
				{
					UserId = i,
					PostLogId = createPost.Id,
					IsActive = true,

				};
				_projectTwoUnitOfWork.ViewPost.Add(addUser);
			}

			_projectTwoUnitOfWork.Save();


		}

		public List<ViewPostDTO> GetUser(int logedinUserId, int postId)
		{
			var j = _projectTwoUnitOfWork.ViewPost.Query(x => x.PostLogId == postId && x.IsActive).ToList();

			var postToUpdate = new List<ViewPostDTO> { };

			foreach(var p in j)
			{
				var queriedPost = _projectTwoUnitOfWork.User.Query(x => x.Id == p.UserId).FirstOrDefault();

				var post = new ViewPostDTO
				{
					Id = p.Id,
					FullName = queriedPost.Name + ' ' + queriedPost.Surname,
					FriendPfp = queriedPost.ImageUrl,
					FriendUserId = queriedPost.Id
				};
				postToUpdate.Add(post);

			};
				
			return postToUpdate;
		}

		public void DeleteFriendAccess(int photoId, int friendUserId)
		{
			var modUser = _projectTwoUnitOfWork.ViewPost.Query(x => x.PostLogId == photoId && x.UserId == friendUserId && x.IsActive).SingleOrDefault();

			modUser.IsActive = false;
			

			_projectTwoUnitOfWork.Save();
		}


		public void AddFriendToPhoto(int photoId, int friendId)
		{
			var addUser = new ViewPost { };

			addUser = new ViewPost
			{
				UserId = friendId,
				PostLogId = photoId,
				IsActive = true,

			};
			_projectTwoUnitOfWork.ViewPost.Add(addUser);

			_projectTwoUnitOfWork.Save();


		}
	}

}
