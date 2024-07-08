using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using System.Text;

namespace CloudSync.Common.Blob
{
    public class BlobFileUtil
    {
        #region properties
        internal string BlobConnectionString { get; private set; }
        internal string BlobSasToken { get; private set; }
        #endregion


        #region constructor / dispose
        public BlobFileUtil(string blobConnectionString, string blobSasToken)
        {
            if (string.IsNullOrEmpty(blobConnectionString))
            {
                throw new ArgumentException(nameof(blobConnectionString));
            }

            BlobConnectionString = blobConnectionString;
            BlobSasToken = blobSasToken;
        }

        public void Dispose()
        {
            BlobConnectionString = null;
            BlobSasToken = null;
        }
        #endregion

        #region methods
        public BlockBlobTransferStatus UploadFileInChunks(
            string fullFilePath, BlockBlobTransferStatus transferStatus)
        {
            if (transferStatus == null)
            {
                transferStatus = new BlockBlobTransferStatus();
            }

            var fileToUpload = new FileInfo(fullFilePath);

            BlockBlobClient blobClient;
            try
            {
                if (transferStatus.UploadedBlockIds == null)
                {
                    transferStatus.UploadedBlockIds = new List<string>();
                }

                if (string.IsNullOrEmpty(transferStatus.FileName))
                {
                    transferStatus.FileName = GenerateBlobFileName(fileToUpload.Name);
                }

                var containerClient = CreateBlobContainerClient();
                if (!containerClient.Exists())
                {
                    containerClient.Create();
                }

                blobClient = containerClient.GetBlockBlobClient(transferStatus.FileName);

                if (transferStatus.BlocksUploaded > 0)
                {
                    RestoreBlockIdsList(transferStatus);

                    bool blocksAreStillHere = CheckUploadedBlocks(blobClient, transferStatus.BlocksUploaded);
                    if (!blocksAreStillHere)
                    {
                        //Something is wrong, we have to upload file again from begin.
                        transferStatus.UploadedBlockIds.Clear();
                        transferStatus.FileName = GenerateBlobFileName(fileToUpload.Name);
                    }
                }

                UploadFileBlocks(blobClient, transferStatus, fileToUpload.FullName);
            }
            catch (Exception e)
            {
                transferStatus.LastException = e;
                transferStatus.Result = BlockBlobTransferResult.Partially_Succeeded;
                transferStatus.BlocksUploaded = transferStatus.UploadedBlockIds.Count;
                return transferStatus;
            }

            try
            {
                var headers = new BlobHttpHeaders()
                {
                    ContentType = GetContentType(transferStatus.FileName)
                };
                blobClient.CommitBlockList(transferStatus.UploadedBlockIds, headers);
                transferStatus.CommitCount++;
            }
            catch (Exception e)
            {
                transferStatus.LastException = e;
                transferStatus.CommitCount++;
                if (transferStatus.CommitCount > 3)
                {
                    transferStatus.Result = BlockBlobTransferResult.Failed;
                }
                else
                {
                    transferStatus.Result = BlockBlobTransferResult.Partially_Succeeded;
                }
                return transferStatus;
            }

            transferStatus.Result = BlockBlobTransferResult.Complete;
            transferStatus.LastException = null;
            return transferStatus;
        }

        private void RestoreBlockIdsList(BlockBlobTransferStatus transferStatus)
        {
            for (int i = 0; i < transferStatus.BlocksUploaded; i++)
            {
                transferStatus.UploadedBlockIds.Add(
                    Convert.ToBase64String(Encoding.UTF8.GetBytes(i.ToString("d6"))));
            }
        }

        private bool CheckUploadedBlocks(BlockBlobClient blobClient, int uploadedBlocksCount)
        {
            try
            {
                int? uncommittedBlocksCount = blobClient?
                    .GetBlockList(BlockListTypes.All)?
                    .Value?
                    .UncommittedBlocks?
                    .Count();

                int uncommittedBlocksOnServer = uncommittedBlocksCount.HasValue ? uncommittedBlocksCount.Value : 0;
                bool res = uncommittedBlocksOnServer == uploadedBlocksCount;
                if (!res)
                {
                    var ex =
                        new Exception($"Uncommitted blocks in blob storage: {uncommittedBlocksOnServer}, while expected: {uploadedBlocksCount}");
                }
                return res;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void UploadFileBlocks(
            BlockBlobClient blobClient,
            BlockBlobTransferStatus transferStatus,
            string fileName)
        {
            int blockSize = transferStatus.BlockSize * 1024 * 1024;
            int offset = blockSize * transferStatus.UploadedBlockIds.Count;
            int counter = transferStatus.BlocksUploaded;

            using (var fs = File.OpenRead(fileName))
            {
                if (offset > 0)
                {
                    fs.Seek(offset, SeekOrigin.Begin);
                }

                var bytesRemaining = fs.Length - offset;
                do
                {
                    var dataToRead = Math.Min(bytesRemaining, blockSize);
                    byte[] data = new byte[dataToRead];
                    var dataRead = fs.Read(data, 0, (int)dataToRead);
                    bytesRemaining -= dataRead;
                    if (dataRead > 0)
                    {
                        var blockId = Convert.ToBase64String(Encoding.UTF8.GetBytes(counter.ToString("d6")));
                        blobClient.StageBlock(blockId, new MemoryStream(data));
                        transferStatus.UploadedBlockIds.Add(blockId);
                        transferStatus.BlocksUploaded = transferStatus.UploadedBlockIds.Count;
                        counter++;
                    }
                }
                while (bytesRemaining > 0);
            }
        }

        private string GetContentType(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                if (fileName.EndsWith(".7z", StringComparison.OrdinalIgnoreCase))
                {
                    return "application/x-7z-compressed";
                }
                else if (fileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
                {
                    return "application/x-zip-compressed";
                }
            }

            return "application/octet-stream";
        }

        public string UploadFile(string fullFilePath, string filePrefix = null)
        {
            var fileToUpload = new FileInfo(fullFilePath);
            try
            {
                var containerClient = CreateBlobContainerClient();
                if (!containerClient.Exists())
                {
                    containerClient.Create();
                }

                var uniqueFileName = filePrefix == null ? GenerateBlobFileName(fileToUpload.Name) : $"{filePrefix}_{fileToUpload.Name}";
                var blobClient = containerClient.GetBlobClient(uniqueFileName);
                using (var uploadFileStream = File.OpenRead(fileToUpload.FullName))
                {
                    blobClient.Upload(uploadFileStream, true);
                    uploadFileStream.Close();
                }

                var param = BlobSasToken;
                if (param != null && !param.StartsWith("?"))
                {
                    param = $"?{param}";
                }

                var link = $"{blobClient.Uri.AbsoluteUri}{param}";
                return uniqueFileName;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private BlobContainerClient CreateBlobContainerClient(
            string containerName = "cs-pushes")
        {
            var connectionString = BlobConnectionString;

            // TODO: TokenAuth
            //var blobServiceClient = eventArgs.CustomClient ?? new BlobServiceClient(connectionString);
            var tenantId = "";
            var clientId = "";
            var clientSecret = "";
            var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
            var uri = new Uri("");
            var blobServiceClient = new BlobServiceClient(uri, credential);

            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            return containerClient;
        }

        private static string GenerateBlobFileName(string fileName)
        {
            return $"{DateTime.Now:yyyy-MM-dd-HH-mm-ssfff}_{fileName}";
        }

        public bool DownloadDirectory(string blobContainerName, string toDir)
        {
            try
            {
                var containerClient = CreateBlobContainerClient(blobContainerName);
                var subItems = containerClient.GetBlobs();
                foreach (var cur in subItems)
                {
                    var fileClient = containerClient.GetBlobClient(cur.Name);
                    DownloadFile(fileClient, toDir);
                }

                return true;
            }
            catch (Exception e)
            {
            }
            return false;
        }

        public FileInfo DownloadFile(string fileName, string toDir,
            bool deleteFileAfterDownload = true)
        {
            try
            {
                var containerClient = CreateBlobContainerClient();
                var client = containerClient.GetBlobClient(fileName);
                var fullPathToFile = DownloadFile(client, toDir);
                if (deleteFileAfterDownload)
                {
                    try
                    {
                        client.DeleteIfExists();
                    }
                    catch (Exception e)
                    {
                    }
                }
                return new FileInfo(fullPathToFile);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private static string DownloadFile(BlobClient client, string toDir)
        {
            var stream = client.OpenRead();
            string fullPathToFile = null;
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                fullPathToFile = Path.Combine(toDir, client.Name);
                var fi = new FileInfo(fullPathToFile);
                if (!fi.Directory.Exists)
                {
                    fi.Directory.Create();
                }

                using (Stream file = File.Create(fullPathToFile))
                {
                    CopyStream(reader.BaseStream, file);
                }
            }

            return fullPathToFile;
        }

        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }

        public bool DeleteFile(string fileName)
        {
            try
            {
                var containerClient = CreateBlobContainerClient();
                var client = containerClient.GetBlobClient(fileName);
                client.DeleteIfExists();
                return true;
            }
            catch (Exception e)
            {
            }
            return false;
        }
        #endregion
    }
}
