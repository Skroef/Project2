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
    public class AlbumPhotosService : IAlbumPhotosService
	{
        private readonly ProjectTwoContext _projectTwoContext;
        private readonly IProjectTwoUnitOfWork _projectTwoUnitOfWork;

		public AlbumPhotosService(ProjectTwoContext projectTwoContext, IProjectTwoUnitOfWork projectTwoUnitOfWork)
        {
            _projectTwoContext = projectTwoContext;
            _projectTwoUnitOfWork = projectTwoUnitOfWork;
        }

		public List<AlbumPhotosDTO> GetPhotos(int AlbumId)
		{
			 var r = _projectTwoUnitOfWork.AlbumPhotos.Query(x => x.AlbumId == AlbumId && x.IsActive)
													.Include("PostLog")
													.ToList()
													.Select(x => x.DisplayAlbumImgDTO)
													.ToList();

			return r;
		}

		public void DeletePhoto(int photoId, int albumId)
		{
			var photoToDelete = _projectTwoUnitOfWork.AlbumPhotos.Query(x => x.PostLogId == photoId && x.AlbumId == albumId).SingleOrDefault();
			photoToDelete.IsActive = false;


			_projectTwoUnitOfWork.Save();
		}
		
		public void AddPhoto(int photoId, int albumId)
		{
			var photoToSave = new AlbumPhotos
			{
				PostLogId = photoId,
				AlbumId = albumId,
				IsActive = true
			};

			_projectTwoUnitOfWork.AlbumPhotos.Add(photoToSave);

			_projectTwoUnitOfWork.Save();
		}
	}

	}
