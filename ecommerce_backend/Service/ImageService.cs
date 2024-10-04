using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace ecommerce_backend.Service
{
    public class ImageService
    {
        private string _uploadsFolder;
        private string _currentDirect = "images/variants";

        public ImageService()
        {
            _uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" ,_currentDirect);
            Directory.CreateDirectory(_uploadsFolder); // Ensure the directory exists
        }
        public void setDirect(string direct)
        {
            _currentDirect = direct;
            _uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", _currentDirect);
        }
        public async Task<string> HandleImageUpload(IFormFile imageFile)
        {
            // Generate a unique file name for the new image
            var uniqueFileName = Guid.NewGuid() + "_" + imageFile.FileName;
            var filePath = Path.Combine(_uploadsFolder, uniqueFileName);

            // Save the new image to the specified directory
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            // Return the relative URL to the new image
            return Path.Combine(_currentDirect, uniqueFileName);
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
