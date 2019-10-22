using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FoodDeliveryProject.DataAccess.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Edit(T entity);
        void Delete(T entity);
        T GetById(int id);
        IEnumerable<T> ReadAll();
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
    }
}
