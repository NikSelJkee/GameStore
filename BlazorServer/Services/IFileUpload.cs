using BlazorInputFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Services
{
    public interface IFileUpload
    {
        Task<Stream> UploadAsync(IFileListEntry file);
    }
}
