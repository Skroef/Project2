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
    public class AlbumUserAccessController : Controller
    {
        private readonly IAlbumUserAccessService _albumUserAccessService;
        private readonly IAuthService _authService;

        public AlbumUserAccessController(IAlbumUserAccessService albumUserAccessService, IAuthService authService)
        {
            _albumUserAccessService = albumUserAccessService;
            _authService = authService;
        }

		[HttpPost]
		[Route("~/api/AlbumUserAccess/AddFriendToAlbum")]
		[AllowAnonymous]
		public void AddFriendToAlbum(int albumId, int friendId)
		{
			_albumUserAccessService.AddFriendToAlbum(albumId, friendId);
		}

	}
}
