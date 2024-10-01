using ecommerce_backend.Dtos.Image;
using ecommerce_backend.Dtos.NewFolder;
using ecommerce_backend.Dtos.Variant;
using ecommerce_backend.Models;

namespace ecommerce_backend.Mappers
{
    public static class VariantMappers
    {

        public static GetVariantDto ToGetVariantDto(this Variant variant)
        {
            return new GetVariantDto
            {
                VariantId = variant.VariantId,
                ProductId = variant.ProductId,
                VariantName = variant.VariantName,
                importPrice = variant.ImportPrice,
                salePrice = variant.SalePrice,
                Quantity = variant.Quantity,
                Status = variant.Status,
                Images = variant.Images.Select(item => item.ToImageDto(variant.VariantId)).ToList(),
                Values = variant.Values.Select(v=>v.ToVariantValueDto()).ToList()
            };
        }
        public static Variant ToVariantFromCreateDto(this CreateVariantDto createVariant,List<CreateImageDto> listCreateImageDto)
        {

            var variantModel = new Variant
            {
                ProductId = createVariant.ProductId,
                VariantName = createVariant.VariantName,
                ImportPrice = createVariant.importPrice,
                SalePrice = createVariant.salePrice,
                Quantity = 0,
                Status = "out_of_stock"
            };

            variantModel.Images =
                listCreateImageDto.Select(item => item.ToImageModel(variantModel.VariantId)).ToList(); 

            return variantModel;
        }

      /*  public static Variant ToVariantFromUpdateDto(this UpdateVariantDto UpdateVariantDto,int variantId)
        {

            var variantModel = new Variant
            {

                VariantName = UpdateVariantDto.VariantName,
                Quantity = UpdateVariantDto.Quantity,
                Status = UpdateVariantDto.Status,
            };

            variantModel.Images =
                UpdateVariantDto.UpdateImageDtos.Select(item => item.ToModelFromUpdateImage(variantId)).ToList(); 


            return variantModel;
        }*/

          public static async Task<List<string>> UploadFiles(IEnumerable<IFormFile> files, string folderPath)
        {
            var uploadedFilePaths = new List<string>();

            // Kiểm tra nếu thư mục lưu file không tồn tại thì tạo mới
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            foreach (var file in files)
            {
                if (file != null && file.Length > 0)
                {
                    // Tạo đường dẫn đầy đủ cho từng file
                    var filePath = Path.Combine(folderPath, file.FileName);

                    // Lưu file xuống server
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Thêm đường dẫn file đã được upload vào danh sách
                    uploadedFilePaths.Add(filePath);
                }
            }

            return uploadedFilePaths; // Trả về danh sách đường dẫn các file đã upload
        }
      

    }
}
