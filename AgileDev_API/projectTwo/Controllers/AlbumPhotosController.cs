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
    public class AlbumPhotosController : Controller
    {
		private readonly IAlbumPhotosService _albumPhotosService;
		private readonly IAuthService _authService;

		public AlbumPhotosController(IAlbumPhotosService albumPhotosService, IAuthService authService)
		{
			_albumPhotosService = albumPhotosService;
			_authService = authService;
		}

        [HttpGet]
        [Route("~/api/AlbumPhotos/GetPhotos")]
        [AllowAnonymous]
        public ActionResult<string> GetPhotos(int AlbumId)
        {
            return new JsonResult(_albumPhotosService.GetPhotos(AlbumId));
        }

        [HttpPost]
        [Route("~/api/AlbumPhotos/DeletePhoto")]
        [AllowAnonymous]
        public void DeletePhoto(int photoId, int albumId)
        {
            _albumPhotosService.DeletePhoto(photoId, albumId);
        }

        [HttpPost]
        [Route("~/api/AlbumPhotos/AddPhoto")]
        [AllowAnonymous]
        public void AddPhoto(int photoId, int albumId)
        {
            _albumPhotosService.AddPhoto(photoId, albumId);
        }

        //[HttpGet]
        //[Route("~/api/AlbumPhotos/GetPhotos")]
        //[AllowAnonymous]
        //public ActionResult<string> GetPhotos(int AlbumId)
        //{
        //    return new JsonResult(_albumPhotosService.GetPhotos(AlbumId));
        //}

    }
}
