using Data.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public Repository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _applicationDbContext.Set<T>().ToListAsync();
        }
        public async Task<T> GetById(object Id)
        {
            return await _applicationDbContext.Set<T>().FindAsync(Id);
        }
        public IEnumerable<T> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return _applicationDbContext.Set<T>();
        }
        public async Task DeleteAsync(T entity)
        {
            EntityEntry entityEntry = _applicationDbContext.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;
            await CommitAsync();
        }

        public async Task DeleteAsync(Expression<Func<T, bool>> expression)
        {
            var entities = _applicationDbContext.Set<T>().Where(expression).ToList();
            if(entities.Count > 0)
            {
                _applicationDbContext.Set<T>().RemoveRange(entities);
            }
            await CommitAsync();
        }

        public async Task InsertAsync(IEnumerable<T> entity)
        {
            await _applicationDbContext.Set<T>().AddRangeAsync(entity);
           // await _applicationDbContext.SaveChangesAsync(); // Save changes to the database
            await CommitAsync();
        }



        public async Task Insert(T entity)
        {
            await _applicationDbContext.AddAsync(entity);
            await CommitAsync();

        }

        public void Update(T entity)
        {
            EntityEntry entityEntry = _applicationDbContext.Entry<T>(entity);
            entityEntry.State = EntityState.Modified;
        }
        /*public async Task<int> SaveChangesAsync()
        {
            return await _applicationDbContext.SaveChangesAsync();
        }*/
        public virtual IQueryable<T> Table => _applicationDbContext.Set<T>();

        public async Task CommitAsync()
        {
            await _applicationDbContext.SaveChangesAsync();
        }
        public async Task<T> GetSingleByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await _applicationDbContext.Set<T>().FirstOrDefaultAsync(expression);
        }
    }
}
