using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.PosTransactionDetail;
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
        public PosInvoiceVM CreatePostTransaction(PosTransactionMasterRequestVM request)
        {
            PosInvoiceVM posResult= new PosInvoiceVM();
            posResult.tenantId = request.TenantId;
            var newTransaction = request.ToModel(request);
            newTransaction.Date = DateOnly.FromDateTime(DateTime.UtcNow);
            _context.PosTransactionMaster.Add(newTransaction);
            _context.SaveChanges();
            var transactionId = newTransaction.Id;
            var result = _context.postTransactionMasters.Where(m => m.Id == transactionId).Include(s => s.student).Include(s=>s.student.Branch).Include(s=>s.student.Course).FirstOrDefault();
            Student student = new Student();
            if (result != null)
            {
               student.studentId= request.StudentId;
               student.studentName = result.student.FullName;
               student.className=result.student.Course.Name;
               student.rollNo = "MSI-" + result.student.Id.ToString();
               student.branch = result.student.Branch.Name;
            }
            posResult.date = result.Date;
            posResult.student = student;
            posResult.invoiceNumber = "INV-MSI-" + transactionId;
            posResult.status = "Paid";
            posResult.payment.method = request.PaymentMethod;
            posResult.items = request.Items;
            double price = 0;
            double gstValue = 0;

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
                price += Item.UnitPrice;
                gstValue += Item.GstValue;

                transactionDetails.Add(mPosTransactionDetails);

            }


            _context.PosTransactionDetails.AddRange(transactionDetails);
            _context.SaveChanges();
            posResult.subtotal = price;
            posResult.gst = gstValue;
            posResult.total = price + gstValue;

           

            foreach (var Item in request.Items)
            {
                var ItemsList = _context.ItemBranch.Where(i=>i.ItemId == Item.Itemid).First();
                var branchItem = _context.ItemBranch.FirstOrDefault(ib => ib.ItemId == ItemsList.ItemId);

                if (branchItem == null)
                {
                    throw new Exception($"Item with ID {ItemsList.Id} not found");
                }

                if (branchItem.ItemQuantity < ItemsList.ItemQuantity)
                {
                    throw new Exception($"Insufficient stock for Item ID {ItemsList.Id}");
                }

                branchItem.ItemQuantity -= Item.Quantity;

                _context.ItemBranch.Update(branchItem);


            }

            _context.SaveChanges();


            return posResult;
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
