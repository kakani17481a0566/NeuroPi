using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace NeuroPi.UserManagment.ViewModel.Role
{
    public class RoleRequestVM
    {

        public string Name { get; set; }

       

     
        public int CreatedBy { get; set; }

      
        public int TenantId { get; set; }


        //[Column("updated_on")]
        //public DateTime? UpdatedOn { get; set; } = DateTime.UtcNow;


        //[Column("updated_by")]
        //public int UpdatedBy { get; set; }





        public static MRole ToModel(RoleRequestVM roleRequestVM)
        {
            return new MRole
            {
                Name = roleRequestVM.Name,
                TenantId = roleRequestVM.TenantId,
                CreatedBy = roleRequestVM.CreatedBy,
               

                
            };
        }



       
    }
}
