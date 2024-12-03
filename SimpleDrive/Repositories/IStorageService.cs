namespace SimpleDrive.Repositories
{
    public interface IStorageService
    {
        Task StoreBlobAsync(string id, byte[] data);
        Task<byte[]> RetrieveBlobAsync(string id);


    }
}