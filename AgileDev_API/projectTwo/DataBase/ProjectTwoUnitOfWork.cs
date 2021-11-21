using projectTwo.Interfaces;
using projectTwo.Models;
using projectTwo.Database;
using System.Collections.Generic;
using System.Data;

namespace EntityConfigurationBase
{
	public class ProjectTwoUnitOfWork : IProjectTwoUnitOfWork
	{
		private readonly ProjectTwoContext _context;

		
		private IRepository<User> _user;
		private IRepository<Item> _item;
		private IRepository<DragAndDrop> _dragAndDrop;
		private IRepository<UserRole> _userRole;
		private IRepository<PostLog> _postLog;
		private IRepository<Album> _album;
		private IRepository<AlbumPhotos> _albumPhotos;
		private IRepository<AlbumUserAccess> _albumUserAccess;
		private IRepository<PostComment> _postComment;
		private IRepository<ViewPost> _viewPost;
		private IRepository<Friends> _friends;


		public ProjectTwoUnitOfWork(ProjectTwoContext context)
		{
			_context = context;
		}

		public IRepository<User> User => _user ?? (_user = new Repository<User>(_context));
		public IRepository<Item> Item => _item ?? (_item = new Repository<Item>(_context));
		public IRepository<DragAndDrop> DragAndDrop => _dragAndDrop ?? (_dragAndDrop = new Repository<DragAndDrop>(_context));
		public IRepository<UserRole> UserRole => _userRole ?? (_userRole = new Repository<UserRole>(_context));
		public IRepository<PostLog> PostLog => _postLog ?? (_postLog = new Repository<PostLog>(_context));
		public IRepository<Album> Album => _album ?? (_album = new Repository<Album>(_context));
		public IRepository<AlbumPhotos> AlbumPhotos => _albumPhotos ?? (_albumPhotos = new Repository<AlbumPhotos>(_context));
		public IRepository<AlbumUserAccess> AlbumUserAccess => _albumUserAccess ?? (_albumUserAccess = new Repository<AlbumUserAccess>(_context));
		public IRepository<PostComment> PostComment => _postComment ?? (_postComment = new Repository<PostComment>(_context));
		public IRepository<ViewPost> ViewPost => _viewPost ?? (_viewPost = new Repository<ViewPost>(_context));
		public IRepository<Friends> Friends => _friends ?? (_friends = new Repository<Friends>(_context));

		public void Save()
		{
			_context.SaveChanges();
		}

		public void RollbackTransaction()
		{
			_context.RollbackTransaction();
		}

		public void BeginTransaction()
		{
			_context.BeginTransaction();
		}

		public void CommitTransaction()
		{
			_context.CommitTransaction();
		}

		public int ExecuteStoredProcedure(string procName, params object[] parameters)
		{
			// var x = new SqlParameter[]{ new SqlParameter("@name","Tean")};

			return _context.ExecuteStoredProcedure(procName, parameters);
		}

		public List<T> FetchDtoList<T>(
		   string spName,
		   IEnumerable<IDataParameter> parameters)
		   where T : new()
		{
			return _context.FetchDtoList<T>(spName, parameters);
		}

		//public T QueryDatabaseStoredProcedure<T>(string query)
		//{
		//    return _context.Database.SqlQuery<T>(query).FirstOrDefault();
		//}

		//public void ExecuteDatabaseStoredProcedure(string query)
		//{
		//    _context.Database.ExecuteSqlCommand(query);
		//}

		//public List<T> QueryDatabaseStoredProcedureList<T>(string query)
		//{
		//    return _context.Database.SqlQuery<T>(query).ToList();
		//}

	}
}
