using System.ComponentModel;
using System.Reflection;

namespace Blazet.Infrastructure.Security
{
    public static partial class Permissions
    {
        public static List<string> GetRegisteredPermissions()
        {
            var permissions = new List<string>();
            foreach (var prop in typeof(Permissions).GetNestedTypes().SelectMany(c =>
                         c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
            {
                var propertyValue = prop.GetValue(null);
                if (propertyValue is not null)
                    permissions.Add((string)propertyValue);
            }

            return permissions;
        }

        [DisplayName("Audit Trails")]
        [Description("Audit Trails Permissions")]
        public static class AuditTrails
        {
            public const string View = "Permissions.AuditTrails.View";
            public const string Search = "Permissions.AuditTrails.Search";
            public const string Export = "Permissions.AuditTrails.Export";
        }

        [DisplayName("Logs")]
        [Description("Logs Permissions")]
        public static class Logs
        {
            public const string View = "Permissions.Logs.View";
            public const string Search = "Permissions.Logs.Search";
            public const string Export = "Permissions.Logs.Export";
            public const string Purge = "Permissions.Logs.Purge";
        }

        [DisplayName("Picklist")]
        [Description("Picklist Permissions")]
        public static class Dictionaries
        {
            public const string View = "Permissions.Dictionaries.View";
            public const string Create = "Permissions.Dictionaries.Create";
            public const string Edit = "Permissions.Dictionaries.Edit";
            public const string Delete = "Permissions.Dictionaries.Delete";
        }

        [DisplayName("Users")]
        [Description("Users Permissions")]
        public static class Users
        {
            public const string View = "Permissions.Users.View";
            public const string Create = "Permissions.Users.Create";
            public const string Edit = "Permissions.Users.Edit";
            public const string Delete = "Permissions.Users.Delete";
        }

        [DisplayName("Roles")]
        [Description("Roles Permissions")]
        public static class Roles
        {
            public const string View = "Permissions.Roles.View";
            public const string Create = "Permissions.Roles.Create";
            public const string Edit = "Permissions.Roles.Edit";
            public const string Delete = "Permissions.Roles.Delete";
        }


        [DisplayName("Role Claims")]
        [Description("Role Claims Permissions")]
        public static class RoleClaims
        {
            public const string View = "Permissions.RoleClaims.View";
            public const string Create = "Permissions.RoleClaims.Create";
            public const string Edit = "Permissions.RoleClaims.Edit";
            public const string Delete = "Permissions.RoleClaims.Delete";
        }




    }
}