using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

namespace Aceout.Domain.Model.Identity
{
    public enum Permission
    {
        [PermissionInfo("LogOn", IsVisible = false)]
        LogOn,

        [PermissionInfo("Administration")]
        Administration,
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class PermissionInfoAttribute : Attribute
    {
        public string Name { get; }
        public string Group { get; set; }
        public string Description { get; set; }
        public bool IsVisible { get; set; }

        public PermissionInfoAttribute(string name)
        {
            Name = name;
        }
    }

    public static class PermissionExtensions
    {
        public static string GetName(this Permission permission)
        {
            var type = permission.GetType();
            var member = type.GetMember(permission.ToString());
            var attribute = member[0].GetCustomAttribute<PermissionInfoAttribute>();
            return attribute.Name;
        }

        public static string GetDescription(this Permission permission)
        {
            var type = permission.GetType();
            var member = type.GetMember(permission.ToString());
            var attribute = member[0].GetCustomAttribute<PermissionInfoAttribute>();
            return attribute.Description;
        }

    }

    public static class PermissionHelper
    {
        public static IEnumerable<string> GetGroups()
        {
            var type = typeof(Permission);
            var members = type.GetMembers();

            var groups = new HashSet<string>();

            foreach (var member in members)
            {
                var attribute = member.GetCustomAttribute<PermissionInfoAttribute>();
                if (attribute.IsVisible)
                {
                    groups.Add(attribute.Group);
                }
            }

            return groups;
        }
        public static IEnumerable<Permission> GetPermissions(string group)
        {
            var type = typeof(Permission);
            var members = type.GetMembers();
            var values = Enum.GetValues(typeof(Permission)).Cast<Permission>();

            foreach (var member in members)
            {
                var attribute = member.GetCustomAttribute<PermissionInfoAttribute>();

                if (attribute.IsVisible && attribute.Group.Equals(group))
                {
                    yield return values.First(x => x.ToString().Equals(member.Name));
                }
            }

        }
    }


}
