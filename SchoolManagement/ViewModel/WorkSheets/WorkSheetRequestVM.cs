using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.WorkSheets
{
    public class WorkSheetRequestVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int TenantId { get; set; }
        public int CreatedBy { get; set; }

    

    public static MWorksheet ToModel(WorkSheetRequestVM requestVM)
        {
            return new MWorksheet()
            {
                Name = requestVM.Name,
                Description = requestVM.Description,
                Location = requestVM.Location,
                TenantId = requestVM.TenantId,
                CreatedBy = requestVM.CreatedBy,
                CreatedOn = DateTime.UtcNow
            };
        }
    }
}