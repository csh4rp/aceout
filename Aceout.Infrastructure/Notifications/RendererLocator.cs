using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Aceout.Infrastructure.Notifications
{
    public class RendererLocator : IRendererLocator
    {
#pragma warning disable CS0649 // Field 'RendererLocator._assemblies' is never assigned to, and will always have its default value null
        private readonly Assembly[] _assemblies;
#pragma warning restore CS0649 // Field 'RendererLocator._assemblies' is never assigned to, and will always have its default value null

        public IEnumerable<NotificationRenderer<TModel>> GetAll<TModel>()
        {
            foreach (var assembly in _assemblies)
            {
                var assemblyTypes = assembly.GetExportedTypes()
                    .Where(x => x.IsAssignableFrom(typeof(NotificationRenderer<TModel>)));

                foreach (var type in assemblyTypes)
                {
                    yield return Activator.CreateInstance(type) as NotificationRenderer<TModel>;
                }
            }
        }

        public IEnumerable<NotificationInfo> GetInfoList()
        {
            foreach (var assembly in _assemblies)
            {
                var assemblyTypes = assembly.GetExportedTypes()
                    .Where(x => x.IsAssignableFrom(typeof(NotificationRenderer<>)));

                foreach (var type in assemblyTypes)
                {
                    var model = type.GetGenericArguments().First();

                    var properties = model.GetProperties()
                        .Where(x => x.GetCustomAttributes<NotificationVariableAttribute>().Any());

                    var variables = new Dictionary<string, string>();

                    foreach (var property in properties)
                    {
                        var variableAttribute = property.GetCustomAttribute<NotificationVariableAttribute>();
                        variables.Add(variableAttribute.Template, variableAttribute.Description);
                    }

                    var notificationAttribute = type.GetCustomAttribute<NotificationAttribute>();

                    yield return new NotificationInfo
                    {
                        Type = type,
                        Description = notificationAttribute.Description,
                        Name = notificationAttribute.Name,
                        Variables = variables
                    };
                }
            }
        }
    }
}
