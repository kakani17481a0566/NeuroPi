using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.WorkSheets
{
    public class WorkSheetResponseVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int TenantId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }


        public static WorkSheetResponseVM ToViewModel(MWorksheet worksheet)
        {
            return new WorkSheetResponseVM
            {
                Id = worksheet.Id,
                Name = worksheet.Name,
                Description = worksheet.Description,
                Location = worksheet.Location,
                TenantId = worksheet.TenantId,
                CreatedOn = worksheet.CreatedOn,
                CreatedBy = worksheet.CreatedBy, 
                UpdatedBy = worksheet.UpdatedBy,
                UpdatedOn = worksheet.UpdatedOn
            };
        }

        public static List<WorkSheetResponseVM> ToViewModelList(List<MWorksheet> worksheetList)
        {
            List<WorkSheetResponseVM> result = new List<WorkSheetResponseVM>();
            foreach (var worksheet in worksheetList)
            {
                result.Add(ToViewModel(worksheet));
            }
            return result;

        }
    }
}
