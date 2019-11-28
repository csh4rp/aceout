using Aceout.Domain;
using Aceout.Domain.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aceout.Infrastructure.Repositories
{
    public class Repository<TModel, TKey> : IRepository<TModel, TKey> where TModel : Entity<TKey>
    {
        protected ISession Session { get; set; }

        public Repository(ISession session)
        {
            Session = session;
        }

        public void Add(TModel entity)
        {
            Session.Save(entity);
            Session.Flush();
        }

        public void Delete(TKey key)
        {
            var entity = Session.Load<TModel>(key);
            Session.Delete(entity);
            Session.Flush();
        }

        public TModel GetById(TKey key)
        {
            return Session.Get<TModel>(key);
        }

        public void Update(TModel entity)
        {
            Session.Update(entity);
            Session.Flush();
        }

        public Task<TModel> GetByIdAsync(TKey key)
        {
            return Session.GetAsync<TModel>(key);
        }

        public Task AddAsync(TModel entity)
        {
            Session.Save(entity);
            return Session.FlushAsync();
        }

        public Task UpdateAsync(TModel entity)
        {
            Session.Update(entity);
            return Session.FlushAsync();
        }

        public async Task DeleteAsync(TKey key)
        {
            var entity = await Session.LoadAsync<TModel>(key);
            Session.Delete(entity);
            await Session.FlushAsync();
        }

        public void Add(IEnumerable<TModel> entities)
        {
            foreach (var entity in entities)
            {
                Session.Save(entity);
            }

            Session.Flush();
        }

        public Task AddAsync(IEnumerable<TModel> entities)
        {
            foreach (var entity in entities)
            {
                Session.Save(entity);
            }

            return Session.FlushAsync();
        }
    }
}
