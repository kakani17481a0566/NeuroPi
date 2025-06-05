using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.WorkSheets;

namespace SchoolManagement.Services.Implementation
{
    public class WorkSheetServiceImpl : IWorkSheetService
    {
        private readonly SchoolManagementDb _context;
        public WorkSheetServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }
        public WorkSheetResponseVM CreateWorkSheet(WorkSheetRequestVM workSheet)
        {
            var newWorkSheet = WorkSheetRequestVM.ToModel(workSheet);
            newWorkSheet.CreatedOn = DateTime.UtcNow;
            _context.Worksheets.Add(newWorkSheet);
            _context.SaveChanges();
            return WorkSheetResponseVM.ToViewModel(newWorkSheet);

        }

        public bool DeleteWorkSheetByIdAndTenantId(int id, int tenantId)
        {
            var workSheet = _context.Worksheets
                .FirstOrDefault(ws => ws.Id == id && ws.TenantId == tenantId && !ws.IsDeleted);
            if (workSheet == null)
            {
                return false;
            }
            workSheet.IsDeleted = true;
            workSheet.UpdatedOn = DateTime.UtcNow;
            return true;
        }

        public List<WorkSheetResponseVM> GetAllWorkSheets()
        {
            var workSheet = _context.Worksheets
                .Where(ws => !ws.IsDeleted)
                .Select(ws => WorkSheetResponseVM.ToViewModel(ws))
                .ToList();
            return workSheet;
        }

        public WorkSheetResponseVM GetWorkSheetByTenantIdAndId(int tenantId, int id)
        {
            var workSheet = _context.Worksheets
                .FirstOrDefault(ws => ws.TenantId == tenantId && ws.Id == id && !ws.IsDeleted);
            if (workSheet == null)
            {
                return null;
            }
            return WorkSheetResponseVM.ToViewModel(workSheet);
        }

        public WorkSheetResponseVM GetWorkSheetsById(int Id)
        {
            var workSheet = _context.Worksheets
                .FirstOrDefault(ws => ws.Id == Id && !ws.IsDeleted);
            if (workSheet == null)
            {
                return null; 
            }
            return WorkSheetResponseVM.ToViewModel(workSheet);
        }

        public List<WorkSheetResponseVM> GetWorkSheetsByTenantId(int tenantId)
        {
            var workSheet = _context.Worksheets
                .Where(ws => ws.TenantId == tenantId && !ws.IsDeleted)
                .Select(ws => WorkSheetResponseVM.ToViewModel(ws))
                .ToList();
            return workSheet;
        }

        public WorkSheetResponseVM UpdateWorkSheet(int id, int tenantId, WorkSheetUpdateVM workSheet)
        {
            var existingWorkSheet = _context.Worksheets
                .FirstOrDefault(ws => ws.Id == id && ws.TenantId == tenantId && !ws.IsDeleted);
            if (existingWorkSheet == null)
            {
                return null; 
            }
            existingWorkSheet.Name = workSheet.Name;
            existingWorkSheet.Description = workSheet.Description;
            existingWorkSheet.Location = workSheet.Location;
            existingWorkSheet.UpdatedOn = DateTime.UtcNow;
            
            _context.SaveChanges();
            return WorkSheetResponseVM.ToViewModel(existingWorkSheet);

        }
    }
}
