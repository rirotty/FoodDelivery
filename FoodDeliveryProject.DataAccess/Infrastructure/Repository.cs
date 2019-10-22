using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;
using System.Linq.Expressions;

namespace FoodDeliveryProject.DataAccess.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _context;
        private IDbSet<T> _dbSet;

        public Repository(DbContext dbContext)
        {
            _context = dbContext;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);  
        }

        public void Edit(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> ReadAll()
        {
            return _dbSet.ToList();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
    }
}
