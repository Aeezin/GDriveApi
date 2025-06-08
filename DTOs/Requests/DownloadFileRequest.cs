using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DriveAPI.DTOs.Requests
{
    public class DownloadFileRequest
    {
        public required string FileName { get; set; }
    }
}
