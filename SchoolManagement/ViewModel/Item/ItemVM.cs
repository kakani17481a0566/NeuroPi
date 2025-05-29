using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.ViewModel.Item
{
    public class ItemVM
    {
        public int Id { get; set; }

        
        public int ItemHeaderId { get; set; }

        public string BookCondition { get; set; }
        public string Status { get; set; }
        public DateTime? PurchasedOn { get; set; }

       
        public int TenantId { get; set; }

        internal static ItemVM entity(object itemModel)
        {
            throw new NotImplementedException();
        }
    }
}
