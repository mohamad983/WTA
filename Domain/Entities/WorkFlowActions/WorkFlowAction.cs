using Domain.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.Entities.WorkFlowActions
{
    public class WorkFlowAction : BaseEntity
    {
        private static readonly Regex CodePattern =
            new("^[A-Za-z][A-Za-z0-9]{0,49}$", RegexOptions.Compiled);
        public Guid RequestTypeId { get; private set; }
        public string Code { get; private set; } = string.Empty;
        public string Title { get; private set; } = string.Empty;
        public bool IsSystem { get; private set; }
        private WorkFlowAction()
        {

        }
        public WorkFlowAction(Guid requestTypeId, string code, string title, bool isSystem)
        {
            if (requestTypeId == Guid.Empty)
            {
                throw new ArgumentException("Request type Id is required!");
            }
            if (string.IsNullOrWhiteSpace(code) || !CodePattern.IsMatch(code))
            {
                throw new ArgumentException("The code is invalid!");
            }
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("The title is required!");
            }
            RequestTypeId = requestTypeId;
            Code = code.Trim();
            Title = title.Trim();
            IsSystem = isSystem;
        }
        public void Rename(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException();
            }
            Title = title.Trim();
        }
    }
}
