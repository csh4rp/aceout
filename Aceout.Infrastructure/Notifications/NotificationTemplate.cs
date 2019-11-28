using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Infrastructure.Notifications
{
    public class NotificationTemplate
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string Content { get; set; }
        public string Language { get; set; }
        public NotificationType Type { get; set; }
        public bool IsActive { get; set; }
    }
}
