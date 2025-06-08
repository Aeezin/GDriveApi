using DriveAPI.Models;

namespace DriveAPI.Services
{
    public interface IFolderService
    {
        /// <summary>
        /// Skapar en ny mapp kopplad till en användare.
        /// </summary>
        Task<Folder> CreateFolderAsync(string userId, string folderName);

        /// <summary>
        /// Hämtar mapp med tillhörande filer.
        /// </summary>
        Task<Folder> GetFolderByIdAsync(Guid folderId);
    }
}
