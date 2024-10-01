using ecommerce_backend.Dtos.Image;
using ecommerce_backend.Dtos.NewFolder;
using ecommerce_backend.Models;

namespace ecommerce_backend.Mappers
{
    public static class ImageMapper
    {
        public static Image ToImageModel(this CreateImageDto createImageDto, int VariantId)
        {

            return new Image
            {
                VariantId = VariantId,
                ImageUrl = createImageDto.ImageUrl,
            };
        }
        public static Image ToModelFromUpdateImage(this UpdateImageDto updateImageDto, int variantId)
        {
            return new Image
            {
                VariantId = variantId,
                ImageUrl = updateImageDto.ImageUrl
            };
        }



        public static ImageDto ToImageDto(this Image image, int idVariant)
        {
            return new ImageDto
            {
                ImageId = image.ImageId,
                VariantId = idVariant,
                ImageUrl = image.ImageUrl
            };
        }

      

        public static UpdateImageDto ToUpdateImageDtoFromModel(this Image image)
        {
            return new UpdateImageDto
            {
                ImageUrl = image.ImageUrl
            };
        }

        public static async Task<List<CreateImageDto>> UploadListImages(string folderPath, List<IFormFile> files)
        {
            List<CreateImageDto> listImage = new List<CreateImageDto>();
            if (files == null || files.Count == 0)
            {
                return listImage;

            }

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            foreach (var file in files)
            {
                var pathSave = file.HandleUpload(folderPath);
                listImage.Add(new CreateImageDto { ImageUrl = pathSave });
            }

            return listImage;
        }

        public static string HandleUpload(this IFormFile file, string path)
        {
            // Danh sách các phần mở rộng hợp lệ
            List<string> validExtensions = new List<string>() { ".jpg", ".png", ".gif" };
            string extension = Path.GetExtension(file.FileName);

            // Kiểm tra phần mở rộng tệp
            if (!validExtensions.Contains(extension.ToLower()))
            {
                return $"Chỉ cho phép các phần mở rộng: {string.Join(", ", validExtensions)}";
            }

            // Kiểm tra kích thước tệp
            long size = file.Length;
            if (size > 5 * 1024 * 1024)
            {
                return "Kích thước ảnh không quá 5MB";
            }

            // Tạo tên tệp mới và lưu tệp
            string fileName = Guid.NewGuid().ToString() + extension;
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            string fullPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            // Trả về đường dẫn đầy đủ
            return fullPath;
        }
    }
}
