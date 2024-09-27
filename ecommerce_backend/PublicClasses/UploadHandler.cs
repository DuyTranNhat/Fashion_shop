namespace ecommerce_backend.PublicClasses
{
    public static class UploadHandler
    {
        public static string handleUpload(IFormFile file)
        {
            List<string> validExtentions = new List<string>() { ".jpg", ".png", ".gif" };
            string extention = Path.GetExtension(file.FileName);
            if (!validExtentions.Contains(extention))
            {
                return $"Chỉ cho phép các phần mở rộng: ({string.Join(',', validExtentions)})";
            }

            long size = file.Length;
            if (size > 5 * 1024 * 1024)
            {
                return "Kích thước ảnh không quá 5mb";
            }

            string fileName = Guid.NewGuid().ToString() + extention;
            string path = Path.Combine("Assets","Images","Slides");
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            string fullPath = Path.Combine(path, fileName);
            using FileStream stream = new FileStream(fullPath, FileMode.Create);
            file.CopyTo(stream);
            return fullPath;
        }
    }
}
