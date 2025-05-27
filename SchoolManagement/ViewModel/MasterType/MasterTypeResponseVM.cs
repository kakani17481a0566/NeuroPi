using System.ComponentModel.DataAnnotations.Schema;
using SchoolManagement.Model;
using SchoolManagement.ViewModel.Master;

namespace SchoolManagement.ViewModel.MasterType
{
    public class MasterTypeResponseVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TenantId { get; set; }
        public static MasterTypeResponseVM ToViewModel(MMasterType masterType) =>
          new MasterTypeResponseVM
          {
              Id = masterType.Id,
              Name = masterType.Name,
              TenantId = masterType.TenantId,
          };
        public static List<MasterTypeResponseVM> ToViewModelList(List<MMasterType> masterTypes)
        {
            return masterTypes.Select(ToViewModel).ToList();
        }
    }
}
