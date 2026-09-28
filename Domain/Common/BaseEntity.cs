namespace Domain.Common
{
    public abstract class BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public TKey Id { get; protected set; } = default!;

     
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public string? CreatedBy { get; private set; }
        public DateTime? LastModifiedAt { get; private set; }
        public string? LastModifiedBy { get; private set; }

        
        public bool IsDeleted { get; private set; }
        public DateTime? DeletedAt { get; private set; }
        public string? DeletedBy { get; private set; }

        public byte[] RowVersion { get; private set; } = [];

   
        private readonly List<object> _domainEvents = [];
        public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(object domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

    
        public void SetModified(string? modifiedBy = null)
        {
            LastModifiedAt = DateTime.UtcNow;
            LastModifiedBy = modifiedBy;
        }

        public void MarkAsDeleted(string? deletedBy = null)
        {
            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
            DeletedBy = deletedBy;
        }

        #region Entity Equality (برابری موجودیت‌ها بر اساس Id)

        public override bool Equals(object? obj)
        {
            if (obj is not BaseEntity<TKey> other)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            if (EqualityComparer<TKey>.Default.Equals(Id, default!))
                return false;

            return EqualityComparer<TKey>.Default.Equals(Id, other.Id);
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }

        public static bool operator ==(BaseEntity<TKey>? left, BaseEntity<TKey>? right)
        {
            if (left is null && right is null)
                return true;

            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(BaseEntity<TKey>? left, BaseEntity<TKey>? right)
        {
            return !(left == right);
        }

        #endregion
    }
    public abstract class BaseEntity : BaseEntity<Guid>
    {
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }

        protected BaseEntity(Guid id)
        {
            Id = id;
        }
    }
}
