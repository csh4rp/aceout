using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Infrastructure.Database.ChangeTracking
{
    public interface IChangeTracker
    {
        EntityChanges GetChanges(object entity);
    }
}
