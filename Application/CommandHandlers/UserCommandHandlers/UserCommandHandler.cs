using Application.CommandHandlers.UserCommandHandlers.mapper;
using Application.Common.CqrsPattern.CommandAndQueryHandler;
using Application.Dtos.CommandDtos;
using Domain.Common.RepositoryPattern;
using Domain.Entities.Users;
using Domain.Entities.Users.Args;
using Domain.RepoContracts;
using MediatR;

namespace Application.CommandHandlers.UserCommandHandlers
{
    public class UserCommandHandler : ICommandHandler<UserCommandDto, Unit>
    {
        private readonly IUserRepo userRepo;
        private readonly IRoleRepo roleRepo;
        private readonly IPasswordHasher passwordHasher;
        private readonly IUnitOfWorkPattern unitOfWork;

        public UserCommandHandler(IUserRepo userRepo, IPasswordHasher passwordHasher, IRoleRepo roleRepo, IUnitOfWorkPattern unitOfWork)
        {
            this.userRepo = userRepo;
            this.passwordHasher = passwordHasher;
            this.roleRepo = roleRepo;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UserCommandDto request, CancellationToken cancellationToken)
        {
           
            var hashedPassword = passwordHasher.Hash(request.Password);
            var dataUser = UserMap.Map(request, hashedPassword);
            var user = User.New(dataUser);
            var roles = await roleRepo.GetRolesByIdsAsList(request.RoleIds);
            foreach (var role in roles)
            {
                user.AddRole(role);
            }
            await userRepo.AddAsync(user, cancellationToken);
            await unitOfWork.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
