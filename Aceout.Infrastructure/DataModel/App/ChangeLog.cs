using Aceout.Infrastructure.Database.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Infrastructure.DataModel.App
{
    public class ChangeLog
    {
        private EntityChanges _entityChanges;

        public long Id { get; protected set; }
        public DateTime CreatedDate { get; protected set; }
        public int UserId { get; protected set; }
        public string ObjectName { get; protected set; }
        public ChangeAction Action { get; protected set; }
        public string Changes { get; protected set; }

        public ChangeLog(int userId, string objectName, ChangeAction action)
        {
            if (userId <= 0)
                throw new ArgumentException(nameof(UserId));

            if (string.IsNullOrEmpty(objectName))
                throw new ArgumentException(nameof(objectName));

            UserId = userId;
            ObjectName = objectName;
            Action = action;
            CreatedDate = DateTime.UtcNow;
        }

        public void SetChanges(EntityChanges entityChanges)
        {
            Changes = JsonConvert.SerializeObject(entityChanges);
            _entityChanges = null;
        }

        public EntityChanges GetChanges()
        {
            if(_entityChanges == null)
            {
                _entityChanges = JsonConvert.DeserializeObject<EntityChanges>(Changes);
            }

            return _entityChanges;
        }
    }
}
