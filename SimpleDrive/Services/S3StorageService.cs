using SimpleDrive.Repositories;

namespace SimpleDrive.Services
{
    public class S3StorageService : IStorageService
    {
        private readonly HttpClient _httpClient;
        private const string _bucketUrl = "https://s3.example.com/bucket"; 

        public S3StorageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task StoreBlobAsync(string id, byte[] data)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"{_bucketUrl}/{id}")
            {
                Content = new ByteArrayContent(data)
            };
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task<byte[]> RetrieveBlobAsync(string id)
        {
            var response = await _httpClient.GetAsync($"{_bucketUrl}/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}
