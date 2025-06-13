using SchoolManagement.Model;


namespace SchoolManagement.ViewModel.Master
{
    //sai vardhan
    public class MasterRequestVM
    {

        public string Name { get; set; }

        public int MasterTypeId { get; set; }

        public string Code { get; set; }
        public int TenantId { get; set; }

        public int CreatedBy { get; set; }


        public static MMaster ToModel(MasterRequestVM masterRequestVM)
        {
            return new MMaster
            {
                Name = masterRequestVM.Name,
                TenantId = masterRequestVM.TenantId,
                MasterTypeId = masterRequestVM.MasterTypeId,
                CreatedBy = masterRequestVM.CreatedBy,
            };
        }
    }
}
