using SimpleDrive.Repositories;

namespace SimpleDrive.Services
{
    public class LocalStorageService : IStorageService
    {
        private readonly string _storagePath;

        // Constructor accepts a string (path) to the storage directory
        public LocalStorageService(string storagePath)
        {
            _storagePath = storagePath;
        }

        public async Task StoreBlobAsync(string id, byte[] data)
        {
            var filePath = Path.Combine(_storagePath, id);
            await File.WriteAllBytesAsync(filePath, data);
        }

        public async Task<byte[]> RetrieveBlobAsync(string id)
        {
            var filePath = Path.Combine(_storagePath, id);
            return await File.ReadAllBytesAsync(filePath);
        }
    }

}
