using System.ComponentModel.DataAnnotations.Schema;
using SchoolManagement.Model;
using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.visitors
{
    public class VisitorRequestVM
    {
        private DateTime out_time;
        private DateTime in_time;

        //public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string mobileNumber { get; set; }
        public DateTime In_time{ get; set; }
        public DateTime Out_time { get; set; }

        public string Purpose { get; set; }
        public int TenantId { get; set; }

        [Column("created_by")]

        public int CreatedBy { get; set; }

        public static MVisitor ToModel(VisitorRequestVM vm)
        {
            return new MVisitor
            {
                //id = vm.Id,
                name = vm.Name,
                address = vm.Address,
                mobilenumber = vm.mobileNumber,
                in_time = vm.in_time,
                out_time = vm.out_time,
                purpose = vm.Purpose,
                TenantId = vm.TenantId,
                CreatedBy = vm.CreatedBy
            };
        }
    }
}
