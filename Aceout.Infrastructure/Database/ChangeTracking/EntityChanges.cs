using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Infrastructure.Database.ChangeTracking
{
    public class EntityChanges
    {
        public IDictionary<string, object> CurrentState { get; }
        public IDictionary<string, object> PreviousState { get; }

        public EntityChanges()
        {
            CurrentState = new Dictionary<string, object>();
            PreviousState = new Dictionary<string, object>();
        }
    }
}
