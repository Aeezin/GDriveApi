using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DriveAPI.DTOs.Requests
{
    public class CreateFolderRequest
    {
        public string FolderName { get; set; } = string.Empty;
    }
}
