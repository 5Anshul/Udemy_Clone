using Edtech_backend_API.StandaryDictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Edtech_backend_API.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        void Remove(int id);
        void RemoveRange(IEnumerable<T> entity);
        //find
        T Get(int id);
        //display
        IEnumerable<T> GetAll(
            //filter
            Expression<Func<T, bool>> Filter = null,
            //sorting
            Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
            //multiple
            string includeproperties = null  // category,covertype
            );

       IEnumerable<T> GetAllIncluding(
             Expression<Func<T, bool>> filter = null,
             Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
             params Expression<Func<T, object>>[] includeProperties);
        T FirstOrDefault(
            Expression<Func<T, bool>> Filter = null,
            string includeproperties = null// category , coverpages
            );
    }
}
