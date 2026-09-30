using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Permissions
{
    public static class AppPermissions
    {
        public static class Tickets
        {
            public const string View = "Tickets.View";
            public const string Create = "Tickets.Create";
            public const string Close = "Tickets.Close";
            public const string Assign = "Tickets.Assign";
        }

        public static class  Workflows
        {
            public const string ManageRequestTypes = "Workflow.ManageRequestTypes";
        }
        public static class Users
        {
            public const string View = "Users.View";
            public const string Manage = "Users.Manage";
        }

        public static class Roles
        {
            public const string View = "Roles.View";
            public const string Manage = "Roles.Manage";
        }
    }

    public sealed record PermissionDefinition(string Key, string DisplayName, string GroupName);

    public static class PermissionCatalog
    {
        public static readonly IReadOnlyList<PermissionDefinition> All =
        [
            new(AppPermissions.Tickets.View,   "View tickets",   "Tickets"),
            new(AppPermissions.Tickets.Create, "Create tickets", "Tickets"),
            new(AppPermissions.Tickets.Close,  "Close tickets",  "Tickets"),
            new(AppPermissions.Tickets.Assign, "Assign tickets", "Tickets"),

            new(AppPermissions.Workflows.ManageRequestTypes, "Manage request types", "Workflow"),

            new(AppPermissions.Users.View,   "View users",   "Users"),
            new(AppPermissions.Users.Manage, "Manage users", "Users"),

            new(AppPermissions.Roles.View,   "View roles",   "Roles"),
            new(AppPermissions.Roles.Manage, "Manage roles", "Roles"),
        ];
    }
}
