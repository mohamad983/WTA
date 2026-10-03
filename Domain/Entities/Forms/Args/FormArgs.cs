using Domain.Entities.Forms.Enums;
using Domain.Entities.Users;

namespace Domain.Entities.Forms.Args
{
    public class FormArgs
    {
        public int VersionNumber { get; set; }

        public string Title { get; set; } = string.Empty;

        public FormStatus Status { get; set; }
  
    }
}
