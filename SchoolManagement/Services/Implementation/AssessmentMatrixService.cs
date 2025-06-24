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
            // 1. Fetch students
            var studentList = _db.Students
                .Where(s => !s.IsDeleted && s.TenantId == tenantId && s.CourseId == courseId && s.BranchId == branchId)
                .OrderBy(s => s.Id)
                .ToList();

            var studentIds = studentList.Select(s => s.Id).ToList();

            // 2. Fetch assessments linked to timetable
            var assessmentIdList = _db.TimeTableAssessments
                .Where(tt => tt.TimeTableId == timeTableId && !tt.IsDeleted)
                .Select(tt => tt.AssessmentId)
                .Distinct()
                .ToList();

            // 3. Fetch full assessment details
            var assessmentList = _db.Assessments
                .Include(a => a.AssessmentSkill)
                .Where(a => assessmentIdList.Contains(a.Id) && !a.IsDeleted)
                .OrderBy(a => a.Id)
                .ToList();

            // 4. Map headers
            var assessmentNameMap = new Dictionary<int, string>();
            var headerAssessmentMap = new Dictionary<string, int>();

            foreach (var a in assessmentList)
            {
                var skillCode = a.AssessmentSkill?.Code?.Trim() ?? "SKILL";
                var assessmentCode = a.Name?.Trim() ?? "CODE";

                var header = $"{assessmentCode}";


                if (!assessmentNameMap.ContainsKey(a.Id))
                {
                    assessmentNameMap[a.Id] = header;
                    headerAssessmentMap[header] = a.Id;
                }
            }

            // 5. Fetch existing grades
            var dailyAssessmentList = _db.DailyAssessments
                .Where(da => !da.IsDeleted && da.TimeTableId == timeTableId && studentIds.Contains(da.StudentId))
                .Include(da => da.Grade)
                .ToList();

            var assessmentLookup = dailyAssessmentList
                .ToDictionary(
                    da => (da.StudentId, da.AssessmentId),
                    da => da
                );

            // 6. Build headers
            var headers = new List<string> { "Student Name" };
            headers.AddRange(assessmentNameMap.Values);

            // 7. Build matrix rows
            var rows = new List<AssessmentMatrixRow>();
            int serial = 1;

            foreach (var student in studentList)
            {
                var row = new AssessmentMatrixRow
                {
                    SerialNumber = serial++,
                    StudentId = student.Id,
                    StudentName = student.Name,
                    AssessmentGrades = new Dictionary<string, GradeDetail>()
                };

                foreach (var kvp in assessmentNameMap)
                {
                    var assessmentId = kvp.Key;
                    var header = kvp.Value;

                    if (assessmentLookup.TryGetValue((student.Id, assessmentId), out var da))
                    {
                        row.AssessmentGrades[header] = new GradeDetail
                        {
                            GradeId = da.GradeId ?? 0,
                            GradeName = da.Grade?.Name?.Trim() ?? "Not Graded"
                        };
                    }
                    else
                    {
                        row.AssessmentGrades[header] = new GradeDetail
                        {
                            GradeId = 0,
                            GradeName = "Not Graded"
                        };
                    }
                }

                rows.Add(row);
            }

            // 8. Get grade status master values
            var statusList = _db.Masters
                .Where(m => m.MasterTypeId == 40 && !m.IsDeleted)
                .Select(m => new AssessmentStatusVm
                {
                    Id = m.Id,
                    Name = m.Name.Trim()
                })
                .ToList();

            // 9. Final response



            // 10. Get current status id from time_table
            var currentStatusId = _db.TimeTables
    .Where(tt => tt.Id == timeTableId && !tt.IsDeleted)
    .Select(tt => tt.AssessmentStatusCode)
    .FirstOrDefault() ?? 0; // ← this fixes it

            var currentStatusName = _db.Masters
                .Where(m => m.Id == currentStatusId && m.MasterTypeId == 40 && !m.IsDeleted)
                .Select(m => m.Name.Trim())
                .FirstOrDefault() ?? "UNKNOWN";

            return new AssessmentMatrixResponse
            {
                Headers = headers,
                Rows = rows,
                AssessmentStatusCode = statusList,
                HeaderSkillMap = headerAssessmentMap,
                CurrentStatusId = currentStatusId,
                CurrentStatusName = currentStatusName
            };

        }
    }
}
