using EventApp.Services.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EventApp.Services.ImageStorageService
{
    public class ImageStorageService : IImageStorageService
    {
        private readonly List<string> extensions = new List<string>() { ".gif", ".jpg", ".png", ".jpeg", ".svg" };
        private readonly CloudBlobClient blobClient;

        public ImageStorageService(IOptions<StorageSettings> settings)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(settings.Value.StorageConnectionString);
            blobClient = storageAccount.CreateCloudBlobClient();
        }

        public async Task<string> StoreImage(string filename, byte[] array)
        {
            var extension = Path.GetExtension(filename);
            if (!extensions.Contains(extension.ToLower()))
            {
                throw new Exception("Wrong extension");
            }

            CloudBlobContainer container = blobClient.GetContainerReference("eventimages");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(Guid.NewGuid() + extension);

            await blockBlob.UploadFromByteArrayAsync(array, 0, array.Length);

            return blockBlob.Uri.AbsoluteUri;

        }
        public async Task<List<string>> GetImages()
        {
            var blobResultSegment = await blobClient.GetContainerReference("eventimages").ListBlobsSegmentedAsync(null);
            var URIs = new List<string>();
            foreach (var blob in blobResultSegment.Results)
            {
                if (blob is CloudBlockBlob)
                {
                    URIs.Add(blob.Uri.AbsoluteUri);
                }
            }

            return URIs;
        }
    }
}
