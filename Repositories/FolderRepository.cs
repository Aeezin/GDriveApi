using DriveAPI.Data;
using DriveAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DriveAPI.Repositories
{
    public class FolderRepository(AppDbContext context) : IFolderRepository
    {
        public async Task<Folder> CreateAsync(Folder folder)
        {
            context.Folders.Add(folder);
            await context.SaveChangesAsync();
            return folder;
        }

        public async Task<Folder> GetByIdAsync(Guid id)
        {
            return await context.Folders.Include(f => f.Files).FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Folder?> GetByNameAsync(string folderName, string userId)
        {
            return await context
                .Folders.Include(f => f.Files)
                .FirstOrDefaultAsync(f => f.Name == folderName && f.UserId == userId);
        }
    }
}
