namespace ecommerce_backend.Extension
{
    public static class FileExtension
    {
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
