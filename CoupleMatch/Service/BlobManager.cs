using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;

namespace KamiJal.CoupleMatch.Service
{
    public class BlobManager
    {
        private readonly CloudStorageAccount _storageAccount;
        private readonly CloudBlobContainer _cloudBlobContainer;
        private readonly CloudBlobClient _cloudBlobClient;

        private const string StorageConnectionString = "{YOUR_BLOB_STORAGE_CONNECTION_STRING}";
        private const string ContainerName = "{YOUR_BLOB_CONTAINER_NAME}";

        public BlobManager()
        {
            if (!CloudStorageAccount.TryParse(StorageConnectionString, out _storageAccount)) return;

            _cloudBlobClient = _storageAccount.CreateCloudBlobClient();
            _cloudBlobContainer = _cloudBlobClient.GetContainerReference(ContainerName);
        }

        public async Task<string> UploadFileAsync(string blobName, byte[] buffer)
        {
            var cloudBlockBlob = _cloudBlobContainer.GetBlockBlobReference(blobName);
            await cloudBlockBlob.UploadFromByteArrayAsync(buffer, 0, buffer.Length);

            return cloudBlockBlob.Uri.AbsoluteUri;
        }

        public async Task DeleteFileAsync(string blobName) => 
            await _cloudBlobContainer.GetBlockBlobReference(blobName).DeleteIfExistsAsync();

        public async Task<string> GetPhotoUrlByFilename(string filename) => 
            await Task.Run(() => _cloudBlobContainer.GetBlockBlobReference(filename).Uri.AbsoluteUri);

        public async Task GetPhotoByteDataByFilename(string filename, byte[] target) =>
            await _cloudBlobContainer.GetBlockBlobReference(filename).DownloadToByteArrayAsync(target, 0);

    }
}
