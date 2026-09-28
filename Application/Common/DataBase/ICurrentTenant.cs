namespace Application.Common.DataBase
{
    public interface ICurrentTenant
    {
        string TenantId { get; }
        string TenantName { get; }
    }
}
