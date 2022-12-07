using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photor.Core.Contracts
{
    public interface IGoogleDriveService
    {
        public Task<string> UploadImageAsync(IFormFile image);
    }
}
