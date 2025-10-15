using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.PostTransactionMaster;

namespace SchoolManagement.Services.Implementation
{
    public class PosTransactionMasterServiceImpl : IPosTransactionMasterService
    {
        private SchoolManagementDb _context;
        public PosTransactionMasterServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }
        public string CreatePostTransaction(PosTransactionMasterRequestVM request)
        {
            var newTransaction = request.ToModel(request);
            newTransaction.Date = DateOnly.FromDateTime(DateTime.UtcNow);
            _context.PosTransactionMaster.Add(newTransaction);
            _context.SaveChanges();
            var transactionId = newTransaction.Id;
            List<MPosTransactionDetails> transactionDetails = new List<MPosTransactionDetails>();
            foreach(var Item in request.Items)
            {
                MPosTransactionDetails mPosTransactionDetails = new MPosTransactionDetails();
                mPosTransactionDetails.MasterTransactionId = transactionId;
                mPosTransactionDetails.ItemId = Item.Itemid;
                mPosTransactionDetails.Quantity = Item.Quantity;
                mPosTransactionDetails.UnitPrice = Item.UnitPrice;
                mPosTransactionDetails.GstPercentage = Item.GstPercentage;
                mPosTransactionDetails.GstValue = Item.GstValue;

                transactionDetails.Add(mPosTransactionDetails);

            }

            _context.PosTransactionDetails.AddRange(transactionDetails);
            _context.SaveChanges();

            var ItemsList = _context.ItemBranch.ToList();

            foreach (var Item in ItemsList)
            {
                var branchItem = _context.ItemBranch.FirstOrDefault(ib => ib.ItemId == Item.Id);

                if (branchItem == null)
                {
                    throw new Exception($"Item with ID {Item.Id} not found");
                }

                if (branchItem.ItemQuantity < Item.ItemQuantity)
                {
                    throw new Exception($"Insufficient stock for Item ID {Item.Id}");
                }

                branchItem.ItemQuantity -= Item.ItemQuantity;

                _context.ItemBranch.Update(branchItem);


            }

            _context.SaveChanges();


            return "Inserted";
        }

        public List<PosTransactionMasterResponseVM> GetAllPostTransactions()
        {
            throw new NotImplementedException();
        }

        public List<PosTransactionMasterResponseVM> GetPostTransactionById(int studentId)
        {
            var transaction = _context.postTransactionMasters.Include(t => t.student)
                .Include(t=>t.student.Branch)
                .Where(t => t.StudentId == studentId)
                 .Select(t => new PosTransactionMasterResponseVM
                 {
                     Id = t.Id,
                     StudentId = t.StudentId,
                     StudentName = t.student.Name,
                     BranchName = t.student.Branch.Name,
                     Date = t.Date,
                     TenantId =t.TenantId

                 })
                 .ToList();
            if (transaction == null)
            {
                return null;
            }
            return transaction;

        }
    }
}
