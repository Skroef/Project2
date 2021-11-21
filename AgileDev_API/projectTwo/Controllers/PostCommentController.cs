using System.Collections.Generic;
using projectTwo.Interfaces;
using Microsoft.AspNetCore.Mvc;
using projectTwo.DTOs;
using Microsoft.AspNetCore.Authorization;
using System;

namespace projectTwo.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PostCommentController : Controller
	{
		private readonly IPostCommentService _postCommentService;
		private readonly IAuthService _authService;

		public PostCommentController(IPostCommentService postCommentService, IAuthService authService)
		{
			_postCommentService = postCommentService;
			_authService = authService;
		}

		[HttpPost]
		[Route("~/api/Comment/AddComment")]
		[AllowAnonymous]
		public void AddComment(PostCommentDTO comment)
		{
			_postCommentService.AddComment(comment);
		}

		[HttpGet]
		[Route("~/api/Comment/GetComment")]
		[AllowAnonymous]
		public ActionResult<string> GetComment(int logedinUserId)
		{
			return new JsonResult(_postCommentService.GetComment(logedinUserId));
		}


	}
}
