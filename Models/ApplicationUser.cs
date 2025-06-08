using Microsoft.AspNetCore.Identity;

namespace DriveAPI.Models
{
    using Microsoft.AspNetCore.Identity;

    namespace DriveAPI.Models
    {
        public class ApplicationUser : IdentityUser
        {
            public ICollection<Folder> Folders { get; set; } = null!;
            public ICollection<FileEntity> Files { get; set; } = null!;
        }
    }
}
