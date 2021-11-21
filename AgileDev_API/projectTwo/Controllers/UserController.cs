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
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public UserController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpGet("getAllUsersInfo")]
        public ActionResult<string> getAllUsersInfo()
        {
            return new JsonResult(_userService.getAllUsersInfo());
        }

        [HttpPost]
        [Route("~/api/User/registerUser")]
        public void registerUser(RegisterUserDTO registerUser)
        {
            _userService.registerUser(registerUser);
        }

        [HttpGet]
        [Route("~/api/User/GetUserInfo")]
        public ActionResult GetUserInfo(string email)
        {
            return new JsonResult(_userService.getUserInfo(email));
        }

        [HttpGet]
        [Route("~/api/user/GetCurrentUserInfo")]
		[AllowAnonymous]
		public ActionResult<string> GetCurrentUser(int id)
        {
            return new JsonResult(_userService.GetCurrentUser(id));
        }

        [HttpPost]
        [Route("~/api/User/EditUser")]
        [AllowAnonymous]
        public void editUser(UserProfileDTO pack)
        {
            _userService.editUser(pack);
        }

        [HttpGet]
        [Route("~/api/user/GetUserProfileImg")]
        [AllowAnonymous]
        public ActionResult<string>GetUserProfileImg(int logedinUserId)
        {
            return new JsonResult(_userService.GetUserProfileImg(logedinUserId));
        }

        [HttpGet]
        [Route("~/api/user/GetUserProfile")]
        [AllowAnonymous]
        public ActionResult<string> GetUserProfile(int logedinUserId)
        {
            return new JsonResult(_userService.GetUserProfile(logedinUserId));
        }

        [HttpGet]
        [Route("~/api/user/FindFriend")]
        [AllowAnonymous]
        public ActionResult<string> FindFriend(string email)
        {
            return new JsonResult(_userService.FindFriend(email));
        }

        
    }
}
