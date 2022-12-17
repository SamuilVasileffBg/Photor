using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using Microsoft.AspNetCore.Http;
using Photor.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photor.Core.Services
{
    public class GoogleDriveService : IGoogleDriveService
    {
        private const string PathToServiceAccountKeyFile = "../Photor.Core/Services/Credentials/PhotorGoogleDriveCredentials.json";
        private const string ServiceAccountEmail = "photor@photor-370917.iam.gserviceaccount.com";
        private const string UploadFileName = "Test";
        private const string DirectoryId = "17JnAOEecQJhOwFEGtrOs_L9IiYnJ79mR";

        public async Task<string> UploadImageAsync(IFormFile image)
        {
            // Load the Service account credentials and define the scope of its access.
            var credential = GoogleCredential.FromFile(PathToServiceAccountKeyFile)
                            .CreateScoped(DriveService.ScopeConstants.Drive);

            // Create the  Drive service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });

            var name = Guid.NewGuid();

            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = name.ToString(),
                Parents = new List<string>() { DirectoryId }
            };

            string uploadedFileId;
            // Create a new file on Google Drive
                // Create a new file, with metadata and stream.
                var request = service.Files.Create(fileMetadata, image.OpenReadStream(), "image/png, image/jpg, image/jpeg");
                request.Fields = "*";
                var results = await request.UploadAsync(CancellationToken.None);

                if (results.Status == UploadStatus.Failed)
                {
                    Console.WriteLine($"Error uploading file: {results.Exception.Message}");
                }

                // the file id of the new file we created
                uploadedFileId = request.ResponseBody?.Id;
            

            if (String.IsNullOrEmpty(uploadedFileId))
            {
                throw new Exception("Image not uploaded.");
            }

            return @"https://lh3.googleusercontent.com/d/" + uploadedFileId;
        }
    }
}
