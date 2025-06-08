using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DriveAPI.Models.DriveAPI.Models;

namespace DriveAPI.Models
{
    public class Folder
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;

        [JsonIgnore]
        public ICollection<FileEntity> Files { get; set; } = null!;
    }
}
