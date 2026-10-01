using Domain.Common;
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
        public Guid? WorkFlowActionId {  get; private set; }
        public PermissionKind Kind { get; private set; }
        public string? ActionCode { get; private set; }
        public string? ActionTitle { get; private set; }
        private Permission()
        {

        }
        public static Permission CreateStatic(string key, string displayName, string? groupName = null)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new DomainException("Permission key is required.");
            if (string.IsNullOrWhiteSpace(displayName))
                throw new DomainException("Permission display name is required.");

            return new Permission
            {
                Key = key.Trim(),
                DisplayName = displayName.Trim(),
                GroupName = groupName?.Trim(),
                Kind = PermissionKind.Static
            };
        }
        public static Permission CreateForAction(
            Guid requestTypeId, string requestTypeTitle,
            Guid actionId, string actionCode, string actionTitle)
        {
            if (requestTypeId == Guid.Empty)
                throw new DomainException("Request type id is required.");
            if (actionId == Guid.Empty)
                throw new DomainException("Action id is required.");
            if (string.IsNullOrWhiteSpace(requestTypeTitle))
                throw new DomainException("Request type title is required.");
            if (string.IsNullOrWhiteSpace(actionCode))
                throw new DomainException("Action code is required.");
            if (string.IsNullOrWhiteSpace(actionTitle))
                throw new DomainException("Action title is required.");

            var code = actionCode.Trim();
            var requestType = requestTypeTitle.Trim();
            var action = actionTitle.Trim();

            return new Permission
            {
                Key = PermissionKeys.ForAction(requestTypeId, code),
                DisplayName = $"{requestType} - {action}",
                GroupName = requestType,
                Kind = PermissionKind.RequestType,
                RequestTypeId = requestTypeId,
                WorkFlowActionId = actionId,
                ActionCode = code,
                ActionTitle = action
            };
        }

        public void RenameRequestType(string newRequestTypeTitle)
        {
            if (Kind != PermissionKind.RequestType)
                throw new DomainException("Only workflow permissions can be renamed this way.");
            if (string.IsNullOrWhiteSpace(newRequestTypeTitle))
                throw new DomainException("Request type title is required.");

            GroupName = newRequestTypeTitle.Trim();
            DisplayName = $"{GroupName} - {ActionTitle}";
        }

        public void UpdateInfo(string displayName, string? groupName)
        {
            if (Kind != PermissionKind.Static)
                throw new DomainException("Only static permissions can be updated this way.");
            if (string.IsNullOrWhiteSpace(displayName))
                throw new DomainException("Permission display name is required.");

            DisplayName = displayName.Trim();
            GroupName = groupName?.Trim();
        }

        public void RenameAction(string newActionTitle)
        {
            if (Kind != PermissionKind.RequestType)
                throw new DomainException("Only workflow permissions can be renamed this way.");
            if (string.IsNullOrWhiteSpace(newActionTitle))
                throw new DomainException("Action title is required.");

            ActionTitle = newActionTitle.Trim();
            DisplayName = $"{GroupName} - {ActionTitle}";
        }
    }
}
