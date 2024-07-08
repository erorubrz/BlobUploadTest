namespace CloudSync.Common.Blob
{
    public enum BlockBlobTransferResult
    {
        Pending = 0,

        Complete = 1,

        Partially_Succeeded = 2,

        Failed = 3,
    }
}
