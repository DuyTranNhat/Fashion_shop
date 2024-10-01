using ecommerce_backend.DataAccess.Data;
using ecommerce_backend.DataAccess.Repository.IRepository;
using static ecommerce_backend.DataAccess.Repository.IRepository.IVariantRepository;

using ecommerce_backend.Dtos.Receipt;
using ecommerce_backend.Dtos.Slide;
using ecommerce_backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.IdentityModel.Tokens;

namespace ecommerce_backend.DataAccess.Repository
{
    public class ReceiptRepository : Repository<Receipt>, IReceiptRepository
    {
        private readonly FashionShopContext _db;
        public ReceiptRepository(FashionShopContext db) : base(db)
        {
            _db = db;
        }
        public Receipt? Update(int id, UpdateReceiptDto updateReceiptDto)
        {
            var receiptModel = _db.Receipts.Include(x=>x.ReceiptDetails).FirstOrDefault(x => x.ReceiptId == id);

            if (receiptModel == null) return null;
            if (receiptModel.Status != "Pending") return null;
            receiptModel.ReceiptDate = updateReceiptDto.ReceiptDate;
            List<ReceiptDetail> tmpList = receiptModel.ReceiptDetails.ToList();
            updateReceiptDto.ReceiptDetails.ToList().ForEach( updateRD =>
            {
                var existing_receiptDetail = receiptModel.ReceiptDetails.FirstOrDefault(RD => RD.ReceiptId == id && RD.VariantId == updateRD.VariantId);
                if (existing_receiptDetail == null)
                {
                    receiptModel.ReceiptDetails.Add(new ReceiptDetail()
                    {
                        ReceiptId = receiptModel.ReceiptId,
                        VariantId = updateRD.VariantId,
                        Quantity = updateRD.Quantity,
                        UnitPrice = updateRD.UnitPrice,
                    });
                } else
                {
                    existing_receiptDetail.Quantity = updateRD.Quantity;
                    existing_receiptDetail.UnitPrice = updateRD.UnitPrice;
                    tmpList.Remove(existing_receiptDetail);
                }
            });
            if (tmpList.Count > 0) {
                tmpList.ForEach(x => {
                    receiptModel.ReceiptDetails.Remove(x);
                });
            }
            receiptModel.TotalPrice = receiptModel.ReceiptDetails.Sum(rd => rd.UnitPrice * rd.Quantity);
            return receiptModel;
        }

        public Receipt? UpdateStatus(int id, string status)
        {
            var receiptModel = _db.Receipts.Include(r=>r.ReceiptDetails).FirstOrDefault(x => x.ReceiptId == id);
            if (receiptModel == null) return null;
            if (receiptModel.Status == "Completed" || receiptModel.Status == "Expired") return null;
            if (status == "Completed")
            {
                IVariantRepository variantRepository = new VariantRepository(_db);
                variantRepository.UpdateVariantQuantity(receiptModel);
            }
            receiptModel.Status = status;
            return receiptModel;
        }
        public IEnumerable<Models.Receipt>? handleSearch(string keyword)
        {
            bool isNumeric = int.TryParse(keyword, out int parsedId);
            var receiptModels = GetAll(x =>
                isNumeric && x.ReceiptId == parsedId
            , includeProperties: "ReceiptDetails");
            if (receiptModels.IsNullOrEmpty()) return null;
            return receiptModels;
        }


    }
}
