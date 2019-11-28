using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aceout.Domain
{
    public interface IRepository<TEntity, TKey>
    {
        void Add(TEntity entity);
        void Add(IEnumerable<TEntity> entities);
        Task AddAsync(TEntity entity);
        Task AddAsync(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity);
        void Delete(TKey key);
        Task DeleteAsync(TKey key);
        TEntity GetById(TKey key);

        Task<TEntity> GetByIdAsync(TKey key);
    }
}
