using Domain.Entities.Users;

namespace Application.Dtos.CommandDtos.Dtos
{
    public class RoleDto
    {
        public string RoleName { get; set; }

        public int RoleImportance { get; set; }

        public bool isSystem { get; set; }
    }
}
