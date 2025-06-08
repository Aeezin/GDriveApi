using System.Security.Claims;
using DriveAPI.DTOs.Requests;
using DriveAPI.Repositories;
using DriveAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DriveAPI.Controllers
{
    [ApiController]
    [Route("api/folder")]
    public class FolderController(
        IFolderService folderService,
        IFolderRepository folderRepository,
        IFileRepository fileRepository,
        IFileService fileService
    ) : ControllerBase
    {
        [HttpPost("create-folder")]
        public async Task<IActionResult> CreateFolder([FromBody] CreateFolderRequest request)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrWhiteSpace(userId))
                    return Unauthorized("Could not find user");

                var folderName = request.FolderName;

                var folder = await folderService.CreateFolderAsync(userId, folderName);
                return Ok(folder);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong. " + ex.Message);
            }
        }

        [HttpGet("get-folder/{id}")]
        public async Task<IActionResult> GetFolder(Guid id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrWhiteSpace(userId))
                    return Unauthorized("Could not find user.");

                var folder = await folderService.GetFolderByIdAsync(id);
                if (folder == null || folder.UserId != userId)
                    return NotFound();

                return Ok(folder);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong. " + ex.Message);
            }
        }

        [HttpGet("get-all-files")]
        public async Task<IActionResult> GetAllFilesFromFolder([FromQuery] string folderName)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrWhiteSpace(userId))
                    return Unauthorized("Could not find user.");

                if (string.IsNullOrWhiteSpace(folderName))
                    return BadRequest("Folder name is required.");

                var folder = await folderRepository.GetByNameAsync(folderName, userId);
                if (folder == null)
                    return NotFound($"Folder '{folderName}' not found for this user.");

                var files = await fileRepository.GetFilesByFolderIdAsync(folder.Id);

                return Ok(files);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong. " + ex.Message);
            }
        }
    }
}
