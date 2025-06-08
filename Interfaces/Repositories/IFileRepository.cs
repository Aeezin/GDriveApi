using DriveAPI.Models;

namespace DriveAPI.Repositories
{
    public interface IFileRepository
    {
        Task<FileEntity> CreateAsync(FileEntity file);
        Task<FileEntity> GetByIdAsync(Guid id);
        Task DeleteAsync(FileEntity file);
        Task<IEnumerable<FileEntity>> GetFilesByFolderIdAsync(Guid folderId);
    }
}
