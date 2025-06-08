using DriveAPI.Data;
using DriveAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DriveAPI.Repositories
{
    public class FileRepository(AppDbContext context) : IFileRepository
    {
        public async Task<FileEntity> CreateAsync(FileEntity file)
        {
            context.Files.Add(file);
            await context.SaveChangesAsync();
            return file;
        }

        public async Task<FileEntity> GetByIdAsync(Guid id)
        {
            return await context.Files.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task DeleteAsync(FileEntity file)
        {
            context.Files.Remove(file);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FileEntity>> GetFilesByFolderIdAsync(Guid folderId)
        {
            return await context.Files.Where(f => f.FolderId == folderId).ToListAsync();
        }
    }
}
