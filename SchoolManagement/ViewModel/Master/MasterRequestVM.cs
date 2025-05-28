using SchoolManagement.Model;


namespace SchoolManagement.ViewModel.Master
{
    //sai vardhan
    public class MasterRequestVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int MasterTypeId { get; set; }

        public int TenantId { get; set; }

        public static MMaster ToModel(MasterRequestVM masterRequestVM)
        {
            return new MMaster
            {
                Id = masterRequestVM.Id,
                Name = masterRequestVM.Name,
                TenantId = masterRequestVM.TenantId,
                MasterTypeId = masterRequestVM.MasterTypeId
            };
        }
    }
}
