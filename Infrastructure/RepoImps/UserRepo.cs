using Domain.Entities.Users;
using Domain.RepoContracts;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RepoImps
{
    public class UserRepo: RepositoryPattern<User>, IUserRepo
    {
        public UserRepo(DbContext context) : base(context)
        {
        }
    }
}
