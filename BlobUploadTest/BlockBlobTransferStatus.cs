
namespace CloudSync.Common.Blob
{
    public class BlockBlobTransferStatus
    {
        #region properites
        public int BlockSize
        {
            get
            {
                return _blockSize;
            }
            set
            {
                if (value > 4)
                {
                    _blockSize = 4;
                }
                else if (value < 1)
                {
                    _blockSize = 1;
                }
                else
                {
                    _blockSize = value;
                }
            }
        }
        private int _blockSize = 1;

        public int BlocksUploaded { get; set; }

        public int CommitCount { get; set; }

        public string FileName { get; set; }

        public List<string> UploadedBlockIds = new List<string>();

        public Exception LastException { get; internal set; }

        public BlockBlobTransferResult Result { get; set; } = BlockBlobTransferResult.Pending;
        #endregion

        #region constructor
        public BlockBlobTransferStatus()
        {
        }
        public BlockBlobTransferStatus(BlockBlobTransferStatus other)
        {
            BlockSize = other.BlockSize;
            BlocksUploaded = other.BlocksUploaded;
            CommitCount = other.CommitCount;
            FileName = other.FileName;
            UploadedBlockIds = new List<string>(other.UploadedBlockIds);
            Result = other.Result;
            LastException = other.LastException;
        }
        #endregion

        #region methods
        internal void Reset()
        {
            BlockSize = 1;
            BlocksUploaded = 0;
            CommitCount = 0;
            FileName = null;
            UploadedBlockIds?.Clear();
            Result = BlockBlobTransferResult.Pending;
            LastException = null;
        }
        #endregion
    }
}
