using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Infrastructure.Notifications
{
    public class NotificationVariableAttribute : Attribute
    {
        public string Template { get; set; }
        public string Description { get; set; }
    }

}
