using System;
using System.Linq;
using System.Linq.Expressions;
using projectTwo.Database;
using projectTwo.Interfaces;
using projectTwo.Models;

namespace projectTwo.Database
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        private readonly ProjectTwoContext _context;

        public Repository(ProjectTwoContext context)
        {
            _context = context;
        }

        public T GetSingle(Expression<Func<T, bool>> whereCondition)
        {
            return _context.Set<T>().Where(whereCondition).FirstOrDefault();
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public long Count(Expression<Func<T, bool>> whereCondition = null)
        {
            return whereCondition is null ? _context.Set<T>().Count() : _context.Set<T>().Where(whereCondition).Count();
        }

		public bool Any(Expression<Func<T, bool>> whereCondition = null)
		{
			return whereCondition is null ? _context.Set<T>().Any() : _context.Set<T>().Any(whereCondition);
		}

		public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> whereCondition)
        {
            return _context.Set<T>().Where(whereCondition);
        }

        public IQueryable<T> Query()
        {
            return _context.Set<T>();
        }
    }
}
