using Azure.Core;
using AzureBlobTables;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using AzureBlobTables.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using Twilio.TwiML.Voice;
using Azure.Storage.Blobs;

namespace SymphonyWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : Controller
    {
        public class FileUploadResponseData
        {
            public int Id { get; set; }
            public string Status { get; set; }
            public string FileName { get; set; }
            public string ErrorMessage { get; set; }
        }
        public class FileUploadResponse
        {
            public string ErrorMessage { get; set; }
            public List<FileUploadResponseData> Data { get; set; }
        }
        [HttpPost]
        public async Task<FileUploadResponseData> UploadFiles(IFormFile uploadFile)
        {
            //long size = files.Sum(f => f.Length);
            FileUploadResponseData uploadedFile = new FileUploadResponseData();
            string fileName = uploadFile.FileName;
            string completeFileName = string.Empty;
            try
            {

                string name = uploadFile.FileName.Replace(@"\\\\", @"\\");
                if (uploadFile.Length > 0)
                {
                    var memoryStream = new MemoryStream();
                    try
                    {
                        await uploadFile.CopyToAsync(memoryStream);
                        // Upload check if less than 20mb!
                        if (memoryStream.Length < 200097152)
                        {
                            Azure.Storage.Blobs.BlobContainerClient blobContainerClient =
                                    new Azure.Storage.Blobs.BlobContainerClient("BlobUrl...",
                            "blobs");
                            completeFileName = $"blob_{Guid.NewGuid().ToString()}_{fileName}.txt";
                            BlobClient blobClient = blobContainerClient
                                .GetBlobClient(completeFileName);
                            memoryStream.Position = 0;
                            await blobClient.UploadAsync(memoryStream, true);
                            uploadedFile = new FileUploadResponseData()
                            {
                                Id = 1,
                                Status = "OK",
                                FileName = completeFileName,
                                ErrorMessage = "",
                            };
                        }
                        else
                        {
                            uploadedFile = new FileUploadResponseData()
                            {
                                Id = 0,
                                Status = "ERROR",
                                FileName = Path.GetFileName(name),
                                ErrorMessage = "File " + uploadFile + " failed to upload"
                            };
                        }
                    }
                    finally
                    {
                        memoryStream.Close();
                        memoryStream.Dispose();
                    }
                }
                return uploadedFile;
            }
            catch (Exception ex)
            {
                uploadedFile = new FileUploadResponseData()
                {
                    Id = 0,
                    Status = "ERROR",
                    FileName = fileName,
                    ErrorMessage = ex.Message.ToString()
                };
                return uploadedFile;
            }
        }
    }
}
