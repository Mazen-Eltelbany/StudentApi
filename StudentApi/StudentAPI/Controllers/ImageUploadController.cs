using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace StudentAPIWithDB.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadController : Controller
    {
        [HttpPost("Upload")]
        public async Task<IActionResult>UploadImage(IFormFile imageFile)
        {
            if (imageFile == null||imageFile.Length==0)
            {
                return BadRequest("No file uploaded.");
            }
            var UploadDirectory = @"C:\MyUploads";
            var fileName=Guid.NewGuid().ToString()+Path.GetExtension(imageFile.FileName);
            var filePath=Path.Combine(UploadDirectory, fileName);
            if (!Directory.Exists(UploadDirectory))
            {
                Directory.CreateDirectory(UploadDirectory);
            }
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }
            return Ok(new { filePath });

        }
        [HttpGet("GetImage/{fileName}")]
        public IActionResult GetImage(string fileName)
        {
            var uploadDirectory = @"C:\MyUploads";
            var filePath=Path.Combine(uploadDirectory, fileName);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Image Not Found");
            }
            var image=System.IO.File.OpenRead(filePath);
            var mimeType = GetMimeType(filePath);
            return File(image, mimeType);
        }
        private string GetMimeType(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream",
            };
        }
    }
}
