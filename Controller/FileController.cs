using System.Security.Claims;
using DriveAPI.DTOs.Requests;
using DriveAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DriveAPI.Controllers
{
    [ApiController]
    [Route("api/file")]
    public class FileController(IFileService fileService) : ControllerBase
    {
        [HttpPost("upload-file")]
        public async Task<IActionResult> UploadFile([FromBody] UploadFileRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await fileService.UploadFileAsync(
                userId!,
                request.FolderId,
                request.FileName,
                request.Content
            );

            if (result == null)
                return Forbid("Folder not found.");

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> DownloadFile(Guid id)
        {
            User.FindFirstValue(ClaimTypes.NameIdentifier);

            var file = await fileService.DownloadFileAsync(id);
            if (file == null)
                return NotFound();

            return File(file.Content, "application/octet-stream", file.Name);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized("Could not find user.");

            await fileService.DeleteFileAsync(id, userId);
            return NoContent();
        }
    }
}
