using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace ecommerce_backend.Service
{
    public class ImageService
    {
        private readonly string _uploadsFolder;

        public ImageService()
        {
            _uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/slides");
            Directory.CreateDirectory(_uploadsFolder); // Ensure the directory exists
        }

        public async Task<string> HandleImageUpload(IFormFile imageFile, string existingImageUrl)
        {
            // Generate a unique file name for the new image
            var uniqueFileName = Guid.NewGuid() + "_" + imageFile.FileName;
            var filePath = Path.Combine(_uploadsFolder, uniqueFileName);

            // Save the new image to the specified directory
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            // Delete the old image file if it exists
            DeleteOldImage(existingImageUrl);

            // Return the relative URL to the new image
            return Path.Combine("/images/slides", uniqueFileName);
        }

        public void DeleteOldImage(string oldImageUrl)
        {
            if (!string.IsNullOrEmpty(oldImageUrl))
            {
                var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", oldImageUrl.TrimStart('/'));
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }
        }
    }
}
