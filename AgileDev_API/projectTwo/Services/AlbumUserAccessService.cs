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
    public class AlbumUserAccessService : IAlbumUserAccessService
	{
        private readonly ProjectTwoContext _projectTwoContext;
        private readonly IProjectTwoUnitOfWork _projectTwoUnitOfWork;

		public AlbumUserAccessService(ProjectTwoContext projectTwoContext, IProjectTwoUnitOfWork projectTwoUnitOfWork)
        {
            _projectTwoContext = projectTwoContext;
            _projectTwoUnitOfWork = projectTwoUnitOfWork;
        }


		public void AddFriendToAlbum(int albumId, int friendId)
		{
			var create = new AlbumUserAccess
			{
				UserId = friendId,
				AlbumId = albumId,
				IsActive = true,

			};

			_projectTwoUnitOfWork.AlbumUserAccess.Add(create);

			_projectTwoUnitOfWork.Save();
		}

		
	}

}
