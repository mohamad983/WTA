using Domain.Entities.Users;
using Domain.RepoContracts;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RepoImps
{
    public class RoleRepo: RepositoryPattern<Role>,IRoleRepo
    {
        private readonly AppDbContext _context;
        public RoleRepo(DbContext context) : base(context)
        {
            _context = (AppDbContext)context;

        }

        public async Task<List<Role>> GetRolesByIdsAsList(List<Guid> RoleIds)
        {
           return await _context.Roles.Where(x=> RoleIds.Contains(x.Id)).ToListAsync();
        }
    }
}
