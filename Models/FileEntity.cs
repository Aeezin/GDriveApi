using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DriveAPI.Models.DriveAPI.Models;

namespace DriveAPI.Models
{
    public class FileEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public byte[] Content { get; set; } = [];
        public Guid FolderId { get; set; }

        [JsonIgnore]
        public Folder Folder { get; set; } = null!;
        public string UserId { get; set; } = string.Empty;

        [JsonIgnore]
        public ApplicationUser User { get; set; } = null!;
    }
}
