using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Worksheets
{
    public class WorksheetResponseVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int TenantId { get; set; }
        public string TenantName { get; set; }
        public int? SubjectId { get; set; }
        public string SubjectName { get; set; }

        public static WorksheetResponseVM FromModel(MWorksheet model)
        {
            if (model == null) return null;

            return new WorksheetResponseVM
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Location = model.Location,
                TenantId = model.TenantId,
                TenantName = model.Tenant?.Name,
                SubjectId = model.SubjectId,
                SubjectName = model.Subject?.Name
            };
        }
    }
}
