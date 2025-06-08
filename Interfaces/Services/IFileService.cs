using DriveAPI.Models;

namespace DriveAPI.Services
{
    public interface IFileService
    {
        /// <summary>
        /// Skapar ny fil i angiven mapp och kopplar till användare.
        /// </summary>
        Task<FileEntity> UploadFileAsync(
            string userId,
            Guid folderId,
            string fileName,
            byte[] content
        );

        /// <summary>
        /// Hämtar fil för nedladdning.
        /// </summary>
        Task<FileEntity> DownloadFileAsync(Guid fileId);

        /// <summary>
        /// Raderar en fil.
        /// </summary>
        Task DeleteFileAsync(Guid fileId, string userId);
    }
}
