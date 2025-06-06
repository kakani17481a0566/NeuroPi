using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.VwComprehensiveTimeTables;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManagement.Services.Implementation
{
    public class VwComprehensiveTimeTablesService : IVwComprehensiveTimeTablesService
    {
        private readonly SchoolManagementDb _context;

        public VwComprehensiveTimeTablesService(SchoolManagementDb context)
        {
            _context = context;
        }

        public List<VwComprehensiveTimeTableVM> GetAll()
        {
            return _context.VwComprehensiveTimeTables
                .Select(e => new VwComprehensiveTimeTableVM
                {
                    ScheduleDate = e.ScheduleDate,
                    DayOfWeek = e.DayOfWeek,
                    PeriodName = e.PeriodName,
                    SubjectName = e.SubjectName,
                    SubjectCode = e.SubjectCode,
                    TopicName = e.TopicName,
                    TopicCode = e.TopicCode,
                    CourseName = e.CourseName,
                    AssignedWorksheetName = e.AssignedWorksheetName
                })
                .ToList();
        }
    }
}
