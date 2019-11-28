using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Infrastructure.Notifications
{
    public class NotificationAttribute : Attribute
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
