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
	public class ViewPostController : Controller
	{
		private readonly IViewPostService _viewPostService;
		private readonly IAuthService _authService;

		public ViewPostController(IViewPostService viewPostService, IAuthService authService)
		{
			_viewPostService = viewPostService;
			_authService = authService;
		}

		[HttpPost]
		[Route("~/api/ViewPost/AddSpesificUsersToPost")]
		[AllowAnonymous]
		public void AddSpesificUsersToPost(ViewPostDTO specificPostDetails)
		{
			_viewPostService.AddSpesificUsersToPost(specificPostDetails);
		}

		[HttpGet]
		[Route("~/api/ViewPost/GetUser")]
		[AllowAnonymous]
		public ActionResult<string> GetUser(int logedinUserId, int postId)
		{
			return new JsonResult(_viewPostService.GetUser(logedinUserId, postId));
		}

		[HttpPost]
		[Route("~/api/ViewPost/DeleteFriendAccess")]
		public void DeleteFriendAccess(int photoId, int friendUserId)
		{
			_viewPostService.DeleteFriendAccess(photoId, friendUserId);
		}
		
		[HttpPost]
		[Route("~/api/ViewPost/AddFriendToPhoto")]
		public void AddFriendToPhoto(int photoId, int friendId)
		{
			_viewPostService.AddFriendToPhoto(photoId, friendId);
		}


	}
}
