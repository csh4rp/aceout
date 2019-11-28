using Aceout.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Aceout.Infrastructure.Notifications
{
    public abstract class NotificationRenderer<T>
    {

        protected IDictionary<string, string> GetVariables(T model)
        {
            var properties = typeof(T).GetProperties()
                .Where(x => x.GetCustomAttributes<NotificationVariableAttribute>().Any());

            var variables = new Dictionary<string, string>();
            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute<NotificationVariableAttribute>();
                variables.Add(attribute.Template, property.GetValue(model).ToString());
            }

            return variables;
        }

        public virtual string Render(T model, NotificationTemplate notificationTemplate)
        {
            var content = notificationTemplate.Content;
            foreach (var variable in GetVariables(model))
            {
                content = content.Replace(variable.Key, variable.Value);
            }

            return content;
        }

    }
}
