using Domain.Common.Interfaces;

namespace Domain.Common
{
    public abstract class BaseEntity 
    {
        //Id
        public Guid Id { get; protected set; } = Guid.NewGuid();
        // Audit Logs
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public string? CreatedBy { get; private set; }
        public DateTime? LastModifiedAt { get; private set; }
        public string? LastModifiedBy { get; private set; }

        // Soft Delete
        public bool IsDeleted { get; private set; }
        public DateTime? DeletedAt { get; private set; }
        public string? DeletedBy { get; private set; }

        // Concurrency
        public byte[] RowVersion { get; private set; } = [];

        // Domain Events
        private readonly List<IDomainEvent> _domainEvents = [];
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            ArgumentNullException.ThrowIfNull(domainEvent);
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents() => _domainEvents.Clear();

        public void SetModified(string? modifiedBy = null)
        {
            LastModifiedAt = DateTime.UtcNow;
            LastModifiedBy = modifiedBy;
        }
        public void SetModifiedBy(string modifiedBy)
        {
            LastModifiedBy = modifiedBy;
        }

        public virtual void MarkAsDeleted(string? deletedBy = null)
        {
            if (IsDeleted) return;
            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
            DeletedBy = deletedBy;
        }
        public void Restore()
        {
            if (!IsDeleted) return;
            IsDeleted = false;
            DeletedAt = null;
            DeletedBy = null;
        }
    }
}
