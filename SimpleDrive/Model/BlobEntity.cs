namespace SimpleDrive.Model
{
    public class BlobEntity
    {
        public Guid Id { get; set; } // Primary key
        public string BlobId { get; set; }
        public string StorageType { get; set; }
        public long Size { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
