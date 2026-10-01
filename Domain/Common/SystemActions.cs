using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common
{
    public static class SystemActions
    {
        public const string Submit = "Submit";
        public const string View = "View";

        public static IReadOnlyList<(string Code, string Title)> All =
            [

                (Submit,"Submit"),
                (View,"View"),

            ];

        public static class PermissionKeys
        {
            public static string ForRequestType(Guid requestTypeId, string actionCode)
                => $"RequestType.{requestTypeId:N}.{actionCode}";
        }
    }
}
