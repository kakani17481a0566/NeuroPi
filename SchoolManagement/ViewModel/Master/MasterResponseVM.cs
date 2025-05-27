using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Master
{
    //Sai Vardhan
    public class MasterResponseVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TenantId { get; set; }

        public static MasterResponseVM ToViewModel(MMaster master) =>
             new MasterResponseVM
             {
                Id = master.Id,
                Name = master.Name,
                TenantId = master.TenantId,
             };
        public static List<MasterResponseVM> ToViewModelList(List<MMaster> masters)
        {
            return masters.Select(ToViewModel).ToList();
        }

    }
}
