using Aceout.Domain;
using Aceout.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aceout.Test.Utils.Helpes
{
    public class FakeRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : Entity<TKey>
    {
        public List<TEntity> Elements { get; set; }

        public FakeRepository()
        {
            Elements = new List<TEntity>();
        }

        public void Add(TEntity entity)
        {
            Elements.Add(entity);
        }

        public void Delete(TKey key)
        {
            Elements.Remove(Elements.FirstOrDefault(x => x.Id.Equals(key)));
        }

        public TEntity GetById(TKey key)
        {
            return Elements.FirstOrDefault(x => x.Id.Equals(key));
        }

        public void Update(TEntity entity)
        {
            Delete(entity.Id);
            Add(entity);
        }

        public TKey GetId(TEntity entity)
        {
            return entity.Id;
        }

        public Task<TEntity> GetByIdAsync(TKey key)
        {
            return Task.FromResult(GetById(key));
        }

        public Task AddAsync(TEntity entity)
        {
            Add(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(TEntity entity)
        {
            Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(TKey key)
        {
            Delete(key);
            return Task.CompletedTask;
        }

        public void Add(IEnumerable<TEntity> entities)
        {
            foreach (var item in entities)
            {
                Add(item);
            }
        }

        public Task AddAsync(IEnumerable<TEntity> entities)
        {
            foreach (var item in entities)
            {
                Add(item);
            }

            return Task.CompletedTask;
        }
    }
}
