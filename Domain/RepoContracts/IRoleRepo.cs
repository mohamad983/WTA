using Domain.Common.RepositoryPattern;
using Domain.Entities.Users;

namespace Domain.RepoContracts
{
    public interface IRoleRepo:IRepositoryPattern<Role>
    {
        Task<List<Role>> GetRolesByIdsAsList(List<Guid> RoleIds);
    }
}
