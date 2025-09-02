using NeuroPi.UserManagment.Model;
using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.visitors
{
    public class VisitorResponseVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string address { get; set; }
        public string mobileNumber { get; set; }
        public DateTime Date { get; set; }
        public string purpose { get; set; }
        public int TenantId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }


        public static VisitorResponseVM ToViewModel(MVisitor model) => new VisitorResponseVM
        {
            Id = model.id,
            Name = model.name,
            address = model.address,
            mobileNumber = model.mobilenumber,
            purpose = model.purpose,
            TenantId = model.TenantId,
            //CreatedOn = model.CreatedOn,
            //UpdatedOn = (DateTime)model.UpdatedOn
        };



        public static List<VisitorResponseVM> ToViewModelList(List<MVisitor> list)
        {
            return list.Select(ToViewModel).ToList();
        }
        
           
    }
}
