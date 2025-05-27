using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.MasterType
{
    public class MasterTypeRequestVM
    {
        public string Name { get; set; }

        public int TenantId { get; set; }
        
        public int createdBy { get; set; }


        public static MMasterType ToModel(MasterTypeRequestVM requestVM)
        {
            return new MMasterType
            {
                Name = requestVM.Name,
                TenantId = requestVM.TenantId,
                CreatedBy=requestVM.createdBy
            };
        }

    }
    
}
