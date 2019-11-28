using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Infrastructure.Notifications
{
    public class NotificationInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Type Type { get; set; }
        public IDictionary<string, string> Variables { get; set; }
    }
}
