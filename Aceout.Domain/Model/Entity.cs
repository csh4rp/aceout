using System;
using System.Collections.Generic;

namespace Aceout.Domain.Model
{
    public abstract class Entity<T>
    {
        public virtual T Id { get; set; }

        protected Entity() { }

        public override bool Equals(object obj)
        {
            return Equals(obj as Entity<T>);
        }

        protected virtual bool Equals(Entity<T> entity)
        {
            return entity != null && this.Id.Equals(entity.Id);
        }

        public override int GetHashCode()
        {
            return 2108858624 + EqualityComparer<T>.Default.GetHashCode(Id);
        }
    }
}
