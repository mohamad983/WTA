using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Permissions
{
    public class Permission : BaseEntity
    {
        public string Key { get; private set; } = string.Empty;
        public string DisplayName { get; private set; } = string.Empty;
        public string? GroupName { get; private set; } = string.Empty;
        private Permission()
        {

        }
    }
}
