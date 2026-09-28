namespace Application.Common.DataBase
{
    public class CurrentTenant : ICurrentTenant
    {
        public string TenantId => throw new NotImplementedException();

        public string TenantName => throw new NotImplementedException();
    }
}
