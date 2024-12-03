using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleDrive.Model;
using SimpleDrive.Repositories;

namespace SimpleDrive.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("v1/blobs")]
    public class BlobController : ControllerBase
    {
        private readonly IStorageService _storageService;
        private readonly AppDbContext _context;

        public BlobController(IStorageService storageService, AppDbContext context)
        {
            _storageService = storageService;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> StoreBlob([FromBody] Blob blob)
        {
            try
            {
                var data = Convert.FromBase64String(blob.Data);
                await _storageService.StoreBlobAsync(blob.Id, data);

                var blobEntity = new BlobEntity
                {
                    BlobId = blob.Id,
                    StorageType = "Local",
                    Size = data.Length,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Blobs.Add(blobEntity);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Blob stored successfully." });
            }
            catch(Exception ex) {
            {
                return BadRequest("Invalid Base64 data.");
            }}
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> RetrieveBlob(string id)
        {
            var blobEntity = await _context.Blobs.FirstOrDefaultAsync(b => b.BlobId == id);
            if (blobEntity == null)
                return NotFound("Blob not found.");

            var data = await _storageService.RetrieveBlobAsync(id);
            return Ok(new
            {
                Id = id,
                Data = Convert.ToBase64String(data),
                Size = blobEntity.Size,
                CreatedAt = blobEntity.CreatedAt
            });
        }
    }

}
