using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Utilites
{
    public class UtilitesResponseVM
    {
        public int Id{ get; set; }
        public string Name { get; set; }

        public int TenantId { get; set; }

        public static UtilitesResponseVM ToViewModel(MUtilitiesList mUtilitiesList) =>
            new UtilitesResponseVM
            {
                Id = mUtilitiesList.Id,
                Name = mUtilitiesList.Name,
                TenantId = mUtilitiesList.TenantId,
            };
        public static List<UtilitesResponseVM> ToViewModelList(List<MUtilitiesList> mUtilitiesList)
        {
            return mUtilitiesList.Select(ToViewModel).ToList();
        }
    }
}
