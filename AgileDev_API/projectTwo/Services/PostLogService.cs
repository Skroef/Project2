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
    public class PostLogService : IPostLogService
    {
        private readonly ProjectTwoContext _projectTwoContext;
        private readonly IProjectTwoUnitOfWork _projectTwoUnitOfWork;

		public PostLogService(ProjectTwoContext projectTwoContext, IProjectTwoUnitOfWork projectTwoUnitOfWork)
        {
            _projectTwoContext = projectTwoContext;
            _projectTwoUnitOfWork = projectTwoUnitOfWork;
        }


        public void CreatePost(PostLogDTO pack)
        {
			var create = new PostLog
			{
                UserId = pack.UserId,
                ImageUrl = pack.ImageUrl,
                Caption = pack.Caption,
                IsActive = true,
                CreatedOn = DateTime.Now,

            };

			_projectTwoUnitOfWork.PostLog.Add(create);

            _projectTwoUnitOfWork.Save();


            var friends = _projectTwoUnitOfWork.Friends.Query(x => x.ParentId == pack.UserId && x.IsActive)
                .Include("User")
                .ToList();

            var addUser = new ViewPost { };

            foreach (var i in friends)
            {
                addUser = new ViewPost
                {
                    UserId = i.UserId,
                    PostLogId = create.Id,
                    IsActive = true,

                };
                _projectTwoUnitOfWork.ViewPost.Add(addUser);
            }

            _projectTwoUnitOfWork.Save();
		}

  //      public List<PostLogDTO> GetUserLatestPosts(int logedinUserId)
  //      {
		//	return _projectTwoUnitOfWork.PostLog.Query(x => x.UserId == logedinUserId && x.IsActive)
		//		.ToList()
		//		.Select(x => x.DisplayDTO)
		//		.ToList();

		//}

        public List<PostLogDTO> GetUserLatestPosts(int logedinUserId)
        {
            var post = _projectTwoUnitOfWork.PostLog.Query(x => x.UserId == logedinUserId && x.IsActive)
                .ToList()
                .Select(x => x.DisplayDTO)
                .ToList();

            var postToReturn = new List<PostLogDTO> { };
            foreach(var j in post)
			{
                var postComment = _projectTwoUnitOfWork.PostComment.Query(x => x.PostLogId == j.Id && x.IsActive).ToList().Select(x => x.DisplayDTO).ToList();
                

				var postWithComments = new PostLogDTO
				{
					Id = j.Id,
					UserId = j.UserId,
					PostLogId = j.PostLogId,
					ImageUrl = j.ImageUrl,
					Caption = j.Caption,
					CreatedOn = j.CreatedOn,
					FullName = j.FullName,
					FriendPfp = j.FriendPfp,
					postComments = postComment
				};
                postToReturn.Add(postWithComments);

            }
            return postToReturn.OrderByDescending(x => x.CreatedOn).ToList();
        }


        public List<PostLogDTO> GetPostssharedWithMe(int logedinUserId)
        {
            var friendPosts = _projectTwoUnitOfWork.Friends.Query(x => x.UserId == logedinUserId)
                .ToList()
                .Select(x => x.DisplayDTO)
                .ToList();

            var j = new List<PostLogDTO> { };

            var parent = 0;
            foreach (var i in friendPosts)
            {
                parent = i.parentUserId;


                j = _projectTwoUnitOfWork.ViewPost.Query(x => x.UserId == logedinUserId && x.IsActive)
                .Include("User")
                .Include("PostLog")
                .ToList()
                .Select(x => x.DisplayLogDTO)
                .ToList();
            }

            return j;

        }

        //     public List<PostLogDTO> GetUserPostLog(int logedinUserId)
        //     {
        //         var friendPosts = _projectTwoUnitOfWork.Friends.Query(x => x.UserId == logedinUserId)
        //             .ToList()
        //             .Select(x => x.DisplayDTO)
        //             .ToList();

        //         var j = new List<PostLogDTO> { };

        //var parent = 0;
        //foreach (var i in friendPosts)
        //{
        //	parent = i.parentUserId;


        //	j = _projectTwoUnitOfWork.ViewPost.Query(x => x.UserId == logedinUserId && x.IsActive)
        //	.Include("User")
        //	.Include("PostLog")
        //             .Include("PostLog.PostComment")
        //	.ToList()
        //	.Select(x => x.DisplayLogDTO)
        //	.ToList();
        //         }

        //         return j;
        //     }

        public List<PostLogDTO> GetUserPostLog(int logedinUserId)
		{
			var friendPosts = _projectTwoUnitOfWork.Friends.Query(x => x.UserId == logedinUserId)
				.ToList()
				.Select(x => x.DisplayDTO)
				.ToList();

            var j = new List<int> { };

			var parent = 0;
			foreach (var i in friendPosts)
			{
				parent = i.parentUserId;


                j = _projectTwoUnitOfWork.ViewPost.Query(x => x.UserId == logedinUserId && x.IsActive).ToList().Select(x => x.PostLogId).ToList();
			
            }

			var postToReturn = new List<PostLogDTO> { };

            foreach (var ji in j)
            {
                var postComment = _projectTwoUnitOfWork.PostComment.Query(x => x.PostLogId == ji && x.IsActive).Include("User").ToList().Select(x => x.DisplayDTO).ToList();
                var post = _projectTwoUnitOfWork.PostLog.Query(x => x.Id == ji && x.IsActive).Include("User").Include("User.Friends").FirstOrDefault();

                var postWithComments = new PostLogDTO
				{
					Id = ji,
					UserId = post.UserId,
					PostLogId = post.Id,
					ImageUrl = post.ImageUrl,
					Caption = post.Caption,
					CreatedOn = post.CreatedOn,
					FullName = post.User.Name + ' '+ post.User.Surname,
                    //FriendPfp = post.User.Friends.Count() != 0? post.User.Friends.Where(x => x.UserId == post.UserId).SingleOrDefault().User.ImageUrl: null,
                    FriendPfp = post.User.ImageUrl == null ? null :post.User.ImageUrl,
                    postComments = postComment
				};
				postToReturn.Add(postWithComments);

			}
            return postToReturn.OrderByDescending(x => x.CreatedOn).ToList();
        }

        public void UpdatePost(PostLogDTO updateData)
        {
			var postToUpdate = _projectTwoUnitOfWork.PostLog.Query(x => x.Id == updateData.Id).Single();

            postToUpdate.ImageUrl = updateData.ImageUrl;
            postToUpdate.Caption = updateData.Caption;
            //postToUpdate.ImageUrl = updateData.Name;


            _projectTwoUnitOfWork.Save();
		}

        public void DeletePost(int postId)
        {
            var postToDelete = _projectTwoUnitOfWork.PostLog.Query(x => x.Id == postId).Single();
            postToDelete.IsActive = false;
            _projectTwoUnitOfWork.Save();


            var viewPostToDelete = _projectTwoUnitOfWork.ViewPost.Query(x => x.PostLogId == postId).Single();
            viewPostToDelete.IsActive = false;
            _projectTwoUnitOfWork.Save();

            var albumPostToDelete = _projectTwoUnitOfWork.AlbumPhotos.Query(x => x.PostLogId == postId).Single();
            albumPostToDelete.IsActive = false;
            _projectTwoUnitOfWork.Save();
        }
    }

}
