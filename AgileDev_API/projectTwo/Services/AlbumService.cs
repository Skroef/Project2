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
    public class AlbumService : IAlbumService
    {
        private readonly ProjectTwoContext _projectTwoContext;
        private readonly IProjectTwoUnitOfWork _projectTwoUnitOfWork;

		public AlbumService(ProjectTwoContext projectTwoContext, IProjectTwoUnitOfWork projectTwoUnitOfWork)
        {
            _projectTwoContext = projectTwoContext;
            _projectTwoUnitOfWork = projectTwoUnitOfWork;
        }


		public void CreateAlbum(AlbumDTO album) 
		{
			var newAlbum = new Album
			{
				UserId = album.UserId,
				Name = album.Name,
				IsActive = true,
			};

			_projectTwoUnitOfWork.Album.Add(newAlbum);

			_projectTwoUnitOfWork.Save();
		}

		public List<AlbumDTO> GetAlbums(int logedinUserId)
		{
			var r =  _projectTwoUnitOfWork.Album.Query(x => x.UserId == logedinUserId && x.IsActive).ToList().Select(x => x.DisplayDTO).ToList();
			return r;
		}
		
		public List<AlbumDTO> GetAlbumsAsAFriend(int logedinUserId)
		{

			var friendPosts = _projectTwoUnitOfWork.AlbumUserAccess.Query(x => x.UserId == logedinUserId && x.IsActive)
				.ToList()
				.Select(x => x.DisplayDTO)
				.ToList();

			var j = new List<AlbumDTO> { };

			foreach (var i in friendPosts)
			{
				j = _projectTwoUnitOfWork.Album.Query(x => x.Id == i.AlbumId && x.IsActive)
				.Include("User")
				.ToList()
				.Select(x => x.DisplayDTO)
				.ToList();
			}

			return j;

		}

		public void DeleteAlbum(int albumId)
		{
			var albumToDelete = _projectTwoUnitOfWork.Album.Query(x => x.Id == albumId).SingleOrDefault();
			albumToDelete.IsActive = false;


			_projectTwoUnitOfWork.Save();
		}

	}

	}
