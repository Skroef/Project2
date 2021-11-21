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
    public class PostLogController : Controller
    {
        private readonly IPostLogService _postLogService;
        private readonly IAuthService _authService;

        public PostLogController(IPostLogService postLogService, IAuthService authService)
        {
            _postLogService = postLogService;
            _authService = authService;
        }

        [HttpPost]
		[Route("~/api/PostLog/CreatePost")]
        [AllowAnonymous]
        public void CreatePost(PostLogDTO pack)
		{
            _postLogService.CreatePost(pack);
		}

        [HttpGet]
        [Route("~/api/PostLog/GetUserLatestPosts")]
        [AllowAnonymous]
        public ActionResult<string> GetUserLatestPosts(int logedinUserId)
        {
            return new JsonResult(_postLogService.GetUserLatestPosts(logedinUserId));
        }

        [HttpGet]
        [Route("~/api/PostLog/GetPostssharedWithMe")]
        [AllowAnonymous]
        public ActionResult<string> GetPostssharedWithMe(int logedinUserId)
        {
            return new JsonResult(_postLogService.GetPostssharedWithMe(logedinUserId));
        }

        [HttpGet]
        [Route("~/api/user/GetUserPostLog")]
        [AllowAnonymous]
        public ActionResult<string> GetUserPostLog(int logedinUserId)
        {
            return new JsonResult(_postLogService.GetUserPostLog(logedinUserId));
        }

        [HttpPost]
        [Route("~/api/PostLog/UpdatePost")]
        [AllowAnonymous]
        public void UpdatePost(PostLogDTO updateData)
        {
            _postLogService.UpdatePost(updateData);
        }

        [HttpPost]
        [Route("~/api/PostLog/DeletePost")]
        [AllowAnonymous]
        public void DeletePost(int postId)
        {
            _postLogService.DeletePost(postId);
        }
    }
}
