using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.DailyAssessment;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Services.Implementation
{
    public class AssessmentMatrixService : IAssessmentMatrixService
    {
        private readonly SchoolManagementDb _db;

        public AssessmentMatrixService(SchoolManagementDb db)
        {
            _db = db;
        }

        public AssessmentMatrixResponse GetMatrix(int timeTableId, int tenantId, int courseId, int branchId)
        {
            var students = _db.Students
                .Where(s => !s.IsDeleted && s.TenantId == tenantId && s.CourseId == courseId && s.BranchId == branchId)
                .OrderBy(s => s.Id)
                .ToList();

            var studentIds = students.Select(s => s.Id).ToList();

            var dailyAssessments = _db.DailyAssessments
                .Where(d => !d.IsDeleted && studentIds.Contains(d.StudentId) && d.TimeTableId == timeTableId)
                .Include(d => d.Grade)
                .ToList();

            var assessments = _db.TimeTableAssessments
                .Where(x => x.TimeTableId == timeTableId && !x.IsDeleted)
                .Select(x => x.AssessmentId)
                .Distinct()
                .ToList();

            var assessmentNames = _db.Assessments
                .Where(a => assessments.Contains(a.Id) && !a.IsDeleted)
                .OrderBy(a => a.Id)
                .ToDictionary(a => a.Id, a => a.Name);

            var assessmentMap = dailyAssessments
                .GroupBy(d => new { d.StudentId, d.AssessmentId })
                .ToDictionary(g => g.Key, g => g.First());

            var headers = new List<string> { "Student Name" };
            headers.AddRange(assessmentNames.Values.Select(name => name.Trim())); // ✅ Trim header names

            var rows = new List<AssessmentMatrixRow>();
            int serial = 1;

            foreach (var student in students)
            {
                var row = new AssessmentMatrixRow
                {
                    SerialNumber = serial++,
                    StudentId = student.Id,
                    StudentName = student.Name,
                    AssessmentGrades = new Dictionary<string, GradeDetail>()
                };

                foreach (var kv in assessmentNames)
                {
                    var key = new { StudentId = student.Id, AssessmentId = kv.Key };
                    var cleanHeader = kv.Value.Trim(); // ✅ Trim dictionary key

                    if (assessmentMap.TryGetValue(key, out var da))
                    {
                        row.AssessmentGrades[cleanHeader] = new GradeDetail
                        {
                            GradeId = da.GradeId ?? 0,
                            GradeName = da.Grade?.Name ?? "Not Graded"
                        };
                    }
                    else
                    {
                        row.AssessmentGrades[cleanHeader] = new GradeDetail
                        {
                            GradeId = 0,
                            GradeName = "Not Graded"
                        };
                    }
                }

                rows.Add(row);
            }

            var assessmentStatuses = _db.Masters
                .Where(m => m.MasterTypeId == 40 && !m.IsDeleted)
                .Select(m => new AssessmentStatusVm
                {
                    Id = m.Id,
                    Name = m.Name
                })
                .ToList();

            return new AssessmentMatrixResponse
            {
                Headers = headers,
                Rows = rows,
                AssessmentStatusCode = assessmentStatuses
            };
        }
    }
}
