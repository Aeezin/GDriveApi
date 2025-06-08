using DriveAPI.Models;
using DriveAPI.Models.DriveAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DriveAPI.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Folder> Folders { get; set; }
        public DbSet<FileEntity> Files { get; set; }
    }
}
