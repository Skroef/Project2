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
    public class PostCommentService : IPostCommentService
    {
        private readonly ProjectTwoContext _projectTwoContext;
        private readonly IProjectTwoUnitOfWork _projectTwoUnitOfWork;

		public PostCommentService(ProjectTwoContext projectTwoContext, IProjectTwoUnitOfWork projectTwoUnitOfWork)
        {
            _projectTwoContext = projectTwoContext;
            _projectTwoUnitOfWork = projectTwoUnitOfWork;
        }


		public void AddComment(PostCommentDTO comment)
		{
			var create = new PostComment
			{
				UserId = comment.UserId,
				PostLogId = comment.PostLogId,
				Text = comment.Text,
				IsActive = true,

			};

			_projectTwoUnitOfWork.PostComment.Add(create);

			_projectTwoUnitOfWork.Save();
		}

		public List<PostCommentDTO> GetComment(int logedinUserId)
		{
			var j = _projectTwoUnitOfWork.PostComment.Query(x => x.IsActive == true)
				.Include("Postlog")
				.Include("User")
				.ToList()
				.Select(x => x.DisplayDTO)							   
				.ToList();

			return j;


			//return _projectTwoUnitOfWork.PostLog.Query(x => x.UserId == logedinUserId)
			//	.ToList()
			//	.Select(x => x.DisplayDTO)
			//	.ToList();

		}

		//      public List<PostLogDTO> GetUserPostLog(int logedinUserId)
		//      {
		//          var friendPosts = _projectTwoUnitOfWork.Friends.Query(x => x.UserId == logedinUserId)
		//              .ToList()
		//              .Select(x => x.DisplayDTO)
		//              .ToList();

		//          var j = new List<PostLogDTO> { };

		//	var parent = 0;
		//	foreach (var i in friendPosts)
		//	{
		//		parent = i.parentUserId;

		//		j = _projectTwoUnitOfWork.PostLog.Query(x => x.UserId == parent)
		//		.Include("User")
		//		.ToList()
		//		.Select(x => x.DisplayDTO)
		//		.ToList();
		//	}

		//	return j;

		//      }
	}

}
