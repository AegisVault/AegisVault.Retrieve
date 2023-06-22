using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegisVault.Retrieve.Helpers
{
    public class BlobStorageHelper
    {
        public async Task<MemoryStream> GetFile(string Location)
        {
            var connstr = Environment.GetEnvironmentVariable("BlobConnectionString");

            var blobServiceClient = new BlobServiceClient(connstr);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("uploadedfiles");
            BlobClient blobClient = containerClient.GetBlobClient(Location);
            Stream streamDocument = await blobClient.OpenReadAsync();
            MemoryStream ms = new MemoryStream();
            await streamDocument.CopyToAsync(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }
    }
}
