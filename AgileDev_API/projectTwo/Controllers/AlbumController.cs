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
    public class AlbumController : Controller
    {
        private readonly IAlbumService _albumService;
        private readonly IAuthService _authService;

        public AlbumController(IAlbumService albumService, IAuthService authService)
        {
            _albumService = albumService;
            _authService = authService;
        }

        [HttpPost]
        [Route("~/api/Album/CreateAlbum")]
        [AllowAnonymous]
        public void CreateAlbum(AlbumDTO album)
        {
            _albumService.CreateAlbum(album);
        }

        [HttpGet]
        [Route("~/api/Album/GetAlbums")]
        [AllowAnonymous]
        public ActionResult<string> GetAlbums(int logedinUserId)
        {
            return new JsonResult(_albumService.GetAlbums(logedinUserId)); 
        }

        [HttpPost]
        [Route("~/api/Album/DeleteAlbum")]
        [AllowAnonymous]
        public void DeleteAlbum(int albumId)
        {
            _albumService.DeleteAlbum(albumId);
        }

        [HttpGet]
        [Route("~/api/Album/GetAlbumsAsAFriend")]
        [AllowAnonymous]
        public ActionResult<string> GetAlbumsAsAFriend(int logedinUserId)
        {
            return new JsonResult(_albumService.GetAlbumsAsAFriend(logedinUserId));
        }

    }
}
