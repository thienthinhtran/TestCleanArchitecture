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
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> GetByCondition(Expression<Func<T, bool>> expression);
        Task<T> GetById(object Id);
        Task InsertAsync(IEnumerable<T> entity);
        Task Insert(T entity);
        void Update(T entity);
        Task CommitAsync();
        Task<T> GetSingleByConditionAsync(Expression<Func<T, bool>> expression);
    }
}
