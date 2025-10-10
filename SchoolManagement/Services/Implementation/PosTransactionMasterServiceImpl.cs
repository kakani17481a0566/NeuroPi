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



            return "Inserted";
        }

        public List<PosTransactionMasterResponseVM> GetAllPostTransactions()
        {
            throw new NotImplementedException();
        }
    }
}
