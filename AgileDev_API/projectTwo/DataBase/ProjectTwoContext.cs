using projectTwo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
//using Module = projectTwo.Models.Module;

namespace projectTwo.Database
{
	public interface IProjectTwoContext
	{
		DbSet<TEntity> Set<TEntity>() where TEntity : class;
		void BeginTransaction();
		void CommitTransaction();
		void RollbackTransaction();
		int SaveChanges();
		int ExecuteStoredProcedure(string procName, params object[] parameters);
		DbSet<User> User { get; set; }
		DbSet<Item> Item { get; set; }
		DbSet<DragAndDrop> DragAndDrop { get; set; }
		DbSet<UserRole> UserRole { get; set; }
		DbSet<PostLog> PostLog { get; set; }
		DbSet<Album> Album { get; set; }
		DbSet<AlbumPhotos> AlbumPhotos { get; set; }
		DbSet<AlbumUserAccess> AlbumUserAccess { get; set; }
		DbSet<PostComment> PostComment { get; set; }
		DbSet<ViewPost> ViewPost { get; set; }
		DbSet<Friends> Friends { get; set; }




		List<T> FetchDtoList<T>(
			 string spName,
			 IEnumerable<IDataParameter> parameters)
			 where T : new();
	}

	public class ProjectTwoContext : DbContext, IProjectTwoContext
	{
		public ProjectTwoContext(DbContextOptions<ProjectTwoContext> options)
		: base(options)
		{ }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
			  .Where(type => !String.IsNullOrEmpty(type.Namespace))
			  .Where(type => type.BaseType != null && type.BaseType.IsGenericType)
			  .Where(type => type.Namespace == "Fourty5.Infrastructure.Database.Mappings");

			foreach (var type in typesToRegister)
			{
				dynamic configInstance = Activator.CreateInstance(type);
				modelBuilder.ApplyConfiguration(configInstance);
			}
		}

		public DbSet<Item> Item { get; set; }
		public DbSet<User> User { get; set; }
		public DbSet<DragAndDrop> DragAndDrop { get; set; }
		public DbSet<UserRole> UserRole { get; set; }
		public DbSet<PostLog> PostLog { get; set; }
		public DbSet<Album> Album { get; set; }
		public DbSet<AlbumPhotos> AlbumPhotos { get; set; }
		public DbSet<AlbumUserAccess> AlbumUserAccess { get; set; }
		public DbSet<PostComment> PostComment { get; set; }
		public DbSet<ViewPost> ViewPost { get; set; }
		public DbSet<Friends> Friends { get; set; }


		public int ExecuteStoredProcedure(string procName, params object[] parameters)
		{
			return Database.ExecuteSqlCommand(procName, parameters);
		}

		public List<T> FetchDtoList<T>(
			 string spName,
			 IEnumerable<IDataParameter> parameters)
			 where T : new()
		{
			List<T> resultList = new List<T>();

			try
			{
				var cmd = Database.GetDbConnection().CreateCommand();
				cmd.CommandText = spName;
				cmd.CommandType = System.Data.CommandType.StoredProcedure;

				PopulateParameters(cmd, parameters);

				Database.OpenConnection();

				using (IDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						T result = new T();

						foreach (var property in typeof(T).GetProperties())
						{
							if (!IsMapped(property))
							{
								continue;
							}

							var value = GetValue(reader[property.Name]);
							if (value == null)
							{
								property.SetValue(result, null, null);
								continue;
							}

							try
							{
								if (property.PropertyType == typeof(Boolean))
									value = (int)value == 1 ? true : false;

								property.SetValue(result, value, null);

							}
							catch (ArgumentException)
							{
								property.SetValue(result, Activator.CreateInstance(property.MemberType.GetType(), value), null);
							}
							catch (InvalidCastException)
							{
								property.SetValue(result, value, null);
							}
						}

						resultList.Add(result);
					}
				}

				return resultList;
			}
			finally
			{
				Database.CloseConnection();
			}
		}

		private void PopulateParameters(
	IDbCommand command,
	IEnumerable<IDataParameter> parameters)
		{
			if (parameters != null)
			{
				foreach (var parameter in parameters)
				{
					command.Parameters.Add(parameter);
				}
			}
		}

		private bool IsMapped(
		PropertyInfo property)
		{
			return !property.GetCustomAttributes(true).Any(x => x is NotMappedAttribute);
		}
		public object GetValue(
		   object value)
		{
			return (value == null || value is DBNull) ? null : value;
		}


		public void BeginTransaction()
		{
			Database.BeginTransaction();
		}

		public void CommitTransaction()
		{
			Database.CommitTransaction();
		}

		public void RollbackTransaction()
		{
			Database.RollbackTransaction();
			var changedEntriesCopy = this.ChangeTracker.Entries()
				.Where(e => e.State == EntityState.Added ||
				   e.State == EntityState.Modified ||
				   e.State == EntityState.Deleted)
				.ToList();

			foreach (var entry in changedEntriesCopy)
				entry.State = EntityState.Detached;
		}
	}

}
