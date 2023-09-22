using Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Abstraction
{
    public interface IRepository<T> where T : class
    {
        //void DeleteAsync(T entity);
       // void DeleteAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAllAsync();
        /*        Task<IEnumerable<T>> GetAllAsync();
*/
        IEnumerable<T> GetByCondition(Expression<Func<T, bool>> expression);
        /*        Task<int> SaveChangesAsync();
        */
        Task<T> GetById(object Id);
        Task InsertAsync(IEnumerable<T> entity);
       // void Insert(IEnumerable<T> entities);
        void Update(T entity);
        Task CommitAsync();

    }
}
