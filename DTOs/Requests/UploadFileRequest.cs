namespace DriveAPI.DTOs.Requests
{
    public class UploadFileRequest
    {
        public Guid FolderId { get; set; } = Guid.NewGuid();
        public string FileName { get; set; } = string.Empty;
        public byte[] Content { get; set; } = [];
    }
}
