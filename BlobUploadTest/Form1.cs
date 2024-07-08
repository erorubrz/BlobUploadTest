using Azure.Identity;
using Azure.Storage.Blobs;
using System.Text;
using Azure.Storage.Blobs.Specialized;
using CloudSync.Common.Blob;
using Azure.Storage.Blobs.Models;

namespace BlobUploadTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonUploadFile_Click(object sender, EventArgs e)
        {
            textBoxLog.Clear();
            try
            {
                var containerClient = CreateBlobContainerClient();
                if (!containerClient.Exists())
                {
                    containerClient.Create();
                }

                var fi = new FileInfo(textBoxFile.Text);
                var fileName = fi.Name;
                var blobClient = containerClient.GetBlockBlobClient(fileName);
                var transferStatus = new BlockBlobTransferStatus();
                textBoxLog.AppendText(transferStatus.FileName);
                UploadFileBlocks(blobClient, transferStatus, fi.FullName);

                var headers = new BlobHttpHeaders()
                {
                    ContentType = GetContentType(transferStatus.FileName)
                };
                blobClient.CommitBlockList(transferStatus.UploadedBlockIds, headers);

                textBoxLog.AppendText("Upload succeed!");
            }
            catch (Exception ex)
            {
                textBoxLog.AppendText(ex.ToString());
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

        private BlobContainerClient CreateBlobContainerClient()
        {
            var uri = new Uri(textBoxBlobUrl.Text);
            BlobServiceClient blobServiceClient;
            if (checkBoxUseTokenAuth.Checked)
            {
                var tenantId = textBoxTenantId.Text;
                var clientId = textBoxClientId.Text;
                var clientSecret = textBoxClientSecret.Text;
                var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
                blobServiceClient = new BlobServiceClient(uri, credential);
            }
            else
            {
                var connectionString = textBoxBlobCS.Text;
                blobServiceClient = new BlobServiceClient(connectionString);
            }

            string containerName = textBoxBlobContainerName.Text;
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            return containerClient;
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

        private void checkBoxUseTokenAuth_CheckedChanged(object sender, EventArgs e)
        {
            var useToken = checkBoxUseTokenAuth.Checked;
            textBoxTenantId.Enabled = useToken;
            textBoxClientId.Enabled = useToken;
            textBoxClientSecret.Enabled = useToken;
            textBoxBlobCS.Enabled = !useToken;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBoxBlobCS_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxTenantId_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
