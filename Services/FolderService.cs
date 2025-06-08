using DriveAPI.Models;
using DriveAPI.Repositories;

namespace DriveAPI.Services
{
    /// <summary>
    /// Provides folder-related operations such as creation and retrieval.
    /// </summary>
    public class FolderService(IFolderRepository repository) : IFolderService
    {
        /// <summary>
        /// Creates a new folder for the specified user.
        /// </summary>
        /// <param name="userId">The ID of the user who owns the folder.</param>
        /// <param name="folderName">The name of the folder to create.</param>
        /// <returns>The created <see cref="Folder"/> entity.</returns>
        public async Task<Folder> CreateFolderAsync(string userId, string folderName)
        {
            var folder = new Folder { Name = folderName, UserId = userId };
            return await repository.CreateAsync(folder);
        }

        /// <summary>
        /// Retrieves a folder by its unique identifier.
        /// </summary>
        /// <param name="folderId">The ID of the folder to retrieve.</param>
        /// <returns>The <see cref="Folder"/> entity if found; otherwise, <c>null</c>.</returns>
        public async Task<Folder> GetFolderByIdAsync(Guid folderId)
        {
            return await repository.GetByIdAsync(folderId);
        }
    }
}
