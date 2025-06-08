using DriveAPI.Models;
using DriveAPI.Repositories;

namespace DriveAPI.Services
{
    /// <summary>
    /// Provides file management operations such as upload, download, and delete.
    /// </summary>
    public class FileService(IFileRepository fileRepository, IFolderRepository folderRepository)
        : IFileService
    {
        /// <summary>
        /// Uploads a file to a specific folder.
        /// </summary>
        /// <param name="userId">The ID of the user uploading the file.</param>
        /// <param name="folderId">The ID of the folder where the file will be saved.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="content">The content of the file as a byte array.</param>
        /// <returns>The uploaded <see cref="FileEntity"/> if successful; otherwise, <c>null</c> if the folder does not belong to the user or does not exist.</returns>
        public async Task<FileEntity> UploadFileAsync(
            string userId,
            Guid folderId,
            string fileName,
            byte[] content
        )
        {
            var folder = await folderRepository.GetByIdAsync(folderId);
            if (folder == null || folder.UserId != userId)
                return null!;

            var file = new FileEntity
            {
                Name = fileName,
                Content = content,
                FolderId = folderId,
                UserId = userId,
            };
            return await fileRepository.CreateAsync(file);
        }

        /// <summary>
        /// Downloads a file by its ID.
        /// </summary>
        /// <param name="fileId">The ID of the file to download.</param>
        /// <returns>The <see cref="FileEntity"/> if found; otherwise, <c>null</c>.</returns>
        public async Task<FileEntity> DownloadFileAsync(Guid fileId)
        {
            return await fileRepository.GetByIdAsync(fileId);
        }

        /// <summary>
        /// Deletes a file if it belongs to the specified user.
        /// </summary>
        /// <param name="fileId">The ID of the file to delete.</param>
        /// <param name="userId">The ID of the user attempting to delete the file.</param>
        public async Task DeleteFileAsync(Guid fileId, string userId)
        {
            var file = await fileRepository.GetByIdAsync(fileId);
            if (file != null && file.UserId == userId)
            {
                await fileRepository.DeleteAsync(file);
            }
        }
    }
}
