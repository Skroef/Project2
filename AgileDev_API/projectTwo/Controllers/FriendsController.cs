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
    public class FriendsController : Controller
    {
        private readonly IFriendsService _friendsService;
        private readonly IAuthService _authService;

        public FriendsController(IFriendsService friendsService, IAuthService authService)
        {
            _friendsService = friendsService;
            _authService = authService;
        }

        [HttpPost]
        [Route("~/api/Friends/SendFriendRequest")]
        [AllowAnonymous]
        public void SendFriendRequest(SendFriendRequestDTO sendFriend)
        {
            _friendsService.SendFriendRequest(sendFriend);
        }

		[HttpGet]
		[Route("~/api/Friends/GetFriendRequest")]
		public ActionResult GetgetFriendRequest(int logedinUserId)
		{
			return new JsonResult(_friendsService.GetgetFriendRequest(logedinUserId));
		}

        [HttpGet]
		[Route("~/api/Friends/GetSpecificFriend")]
		public ActionResult GetSpecificFriend(int logedinUserId)
		{
			return new JsonResult(_friendsService.GetSpecificFriend(logedinUserId));
		}

        [HttpPost]
        [Route("~/api/Friends/AcceptFriend")]
        [AllowAnonymous]
        public void AcceptFriend(int id)
        {
            _friendsService.AcceptFriend(id);
        }

        [HttpGet]
        [Route("~/api/Friends/GetFriend")]
        public ActionResult GetFriend(int logedinUserId, bool following, bool followers)
        {
            return new JsonResult(_friendsService.GetFriend(logedinUserId, following, followers));
        }

        [HttpGet]
        [Route("~/api/Friends/GetMyFriend")]
        public ActionResult GetMyFriend(int logedinUserId)
        {
            return new JsonResult(_friendsService.GetMyFriend(logedinUserId));
        }
    }
}
