using Domain.Common;
using Domain.Entities.Permissions.Enums;
using Domain.Entities.RequestTypes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using static Domain.Common.SystemActions;

namespace Domain.Entities.Permissions
{
    public class Permission : BaseEntity
    {
        private static readonly Regex ActionCodePattern =
            new("^[A-Za-z][A-Za-z0-9]{0,49}$", RegexOptions.Compiled);
        public string Key { get; private set; } = string.Empty;
        public string DisplayName { get; private set; } = string.Empty;
        public string? GroupName { get; private set; } = string.Empty;
        public Guid? RequestTypeId { get; private set; }
        public PermissionKind Kind { get; private set; }
        public string? ActionCode { get; private set; }
        public string? ActionTitle { get; private set; }
        private Permission()
        {

        }
        public static Permission CreateStatic(string key, string displayName, string? groupName = null)
        {
            return new Permission
            {
                Key = key.Trim(),
                DisplayName = displayName.Trim(),
                GroupName = groupName?.Trim(),
                Kind = PermissionKind.Static
            };
        }
        public static Permission CreateForRequestType(Guid requestTypeId, string requestTypeTitle,string actionCode,string actionTitle)
        {
            if (requestTypeId == Guid.Empty)
                throw new ArgumentException("Request type id is required.", nameof(requestTypeId));
            if (string.IsNullOrWhiteSpace(requestTypeTitle))
                throw new ArgumentException("Request type title is required.", nameof(requestTypeTitle));
            if (string.IsNullOrWhiteSpace(actionCode) || !ActionCodePattern.IsMatch(actionCode.Trim()))
                throw new ArgumentException(
                    "Action code must start with a letter and contain only letters and digits (max 50).",
                    nameof(actionCode));
            if (string.IsNullOrWhiteSpace(actionTitle))
                throw new ArgumentException("Action title is required.", nameof(actionTitle));
            var requestType = requestTypeTitle.Trim();
            var code = actionCode.Trim();
            var action = actionTitle.Trim();
            return new Permission
            {
                Key = PermissionKeys.ForRequestType(requestTypeId,actionCode),
                DisplayName = $"{requestType} - {action}",
                RequestTypeId = requestTypeId,
                Kind = PermissionKind.RequestType,
                ActionCode = code,
                ActionTitle = action
            };
        }

        public void RenameRequestType(string requestTypeTitle)
        {
            if (Kind != PermissionKind.RequestType)
                throw new InvalidOperationException("Only request type permissions can be renamed this way.");
            if (string.IsNullOrWhiteSpace(requestTypeTitle))
                throw new ArgumentException("Request type title is required.", nameof(requestTypeTitle));

            GroupName = requestTypeTitle.Trim();
            DisplayName = $"{GroupName} - {ActionTitle}";
        }

        public void UpdateInfo(string displayName,string? groupName = null)
        {
            if (Kind != PermissionKind.Static)
                throw new InvalidOperationException("Only static permissions can be updated this way.");
            if (string.IsNullOrWhiteSpace(displayName))
                throw new ArgumentException("Display name is required.", nameof(displayName));

            DisplayName = displayName.Trim();
            GroupName = groupName?.Trim();
        }
        
        
    }
}
