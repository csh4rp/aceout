using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Infrastructure.Database.ChangeTracking
{
    public class ChangeTracker : IChangeTracker
    {
        private readonly ISession _session;
        private readonly NHibernate.Engine.ISessionImplementor _implementator;

        public ChangeTracker(ISession session)
        {
            _session = session;
            _implementator = _session.GetSessionImplementation();
        }

        public EntityChanges GetChanges(object entity)
        {
            var entry = _implementator.PersistenceContext.GetEntry(entity);
            var persister = _implementator.Factory.GetEntityPersister(entry.EntityName);

            var old = entry.LoadedState;
            var current = persister.GetPropertyValues(entity);
            var properties = persister.PropertyNames;

            var indexes = persister.FindModified(old, current, entity, _implementator);

            var changes = new EntityChanges();

            foreach (var index in indexes)
            {
                changes.PreviousState[properties[index]] = old[index];
                changes.CurrentState[properties[index]] = current[index];
            }

            return changes;
        }
    }
}
