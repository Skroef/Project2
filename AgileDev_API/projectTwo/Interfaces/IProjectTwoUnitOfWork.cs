using projectTwo.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace projectTwo.Interfaces
{
    public interface IProjectTwoUnitOfWork
	{
		IRepository<User> User { get; }
		IRepository<Item> Item { get; }
		IRepository<DragAndDrop> DragAndDrop { get; }
		IRepository<UserRole> UserRole { get; }
		IRepository<PostLog> PostLog { get; }
		IRepository<PostComment> PostComment { get; }
		IRepository<Album> Album { get; }
		IRepository<AlbumPhotos> AlbumPhotos { get; }
		IRepository<AlbumUserAccess> AlbumUserAccess { get; }
		IRepository<ViewPost> ViewPost { get; }
		IRepository<Friends> Friends { get; }

		void Save();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();

        int ExecuteStoredProcedure(string procName, params object[] parameters);

        List<T> FetchDtoList<T>(
          string spName,
          IEnumerable<IDataParameter> parameters)
          where T : new();
	}
}
