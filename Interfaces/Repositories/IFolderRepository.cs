using DriveAPI.Models;

namespace DriveAPI.Repositories
{
    public interface IFolderRepository
    {
        Task<Folder> CreateAsync(Folder folder);
        Task<Folder> GetByIdAsync(Guid id);
        Task<Folder?> GetByNameAsync(string folderName, string userId);
    }
}
