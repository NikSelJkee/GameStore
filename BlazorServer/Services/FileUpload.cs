using BlazorInputFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Services
{
    public class FileUpload : IFileUpload
    {
        public async Task<Stream> UploadAsync(IFileListEntry file)
        {
            var ms = new MemoryStream();
            await file.Data.CopyToAsync(ms);

            return ms;
        }
    }
}
