using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.DailyAssessment;
using System;
using System.Collections.Generic;
using System.Linq;


// Developed by: Mohith
namespace SchoolManagement.Services.Implementation
{
    public class DailyAssessmentServiceImpl : IDailyAssessmentService
    {
        private readonly SchoolManagementDb _context;

        public DailyAssessmentServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }

        public List<DailyAssessmentResponseVm> GetAll()
        {
            return _context.DailyAssessments
                .Where(x => !x.IsDeleted)
                .Select(DailyAssessmentResponseVm.FromModel)
                .ToList();
        }

        public List<DailyAssessmentResponseVm> GetAllByTenant(int tenantId)
        {
            return _context.DailyAssessments
                .Where(x => !x.IsDeleted && x.TenantId == tenantId)
                .Select(DailyAssessmentResponseVm.FromModel)
                .ToList();
        }

        public DailyAssessmentResponseVm GetById(int id)
        {
            var entity = _context.DailyAssessments.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            return entity != null ? DailyAssessmentResponseVm.FromModel(entity) : null;
        }
        public DailyAssessmentResponseVm GetById(int id, int tenantId)
        {
            var entity = _context.DailyAssessments
                .FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);
            return entity != null ? DailyAssessmentResponseVm.FromModel(entity) : null;
        }

        public DailyAssessmentResponseVm Update(int id, int tenantId, DailyAssessmentUpdateVm request)
        {
            var entity = _context.DailyAssessments
                .FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);

            if (entity == null) return null;

            entity.AssessmentDate = request.AssessmentDate;
            entity.TimeTableId = request.TimeTableId;
            //entity.WorksheetId = request.WorksheetId;
            entity.GradeId = request.GradeId;

            entity.StudentId = request.StudentId;
            //entity.CourseId = request.CourseId;
            entity.ConductedById = request.ConductedById;
            entity.BranchId = request.BranchId;
            entity.UpdatedBy = request.UpdatedBy;
            entity.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();

            return DailyAssessmentResponseVm.FromModel(entity);
        }

        public DailyAssessmentResponseVm Create(DailyAssessmentRequestVm request)
        {
            var model = request.ToModel();
            _context.DailyAssessments.Add(model);
            _context.SaveChanges();
            return DailyAssessmentResponseVm.FromModel(model);
        }

        public bool Delete(int id, int tenantId)
        {
            var entity = _context.DailyAssessments
                .FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);

            if (entity == null) return false;

            entity.IsDeleted = true;
            entity.UpdatedOn = DateTime.UtcNow;
            _context.SaveChanges();
            return true;
        }
        //public AssessmentMatrixResponse GetAssessmentMatrixByTimeTable(int tenantId, int courseId, int branchId, int timeTableId)
        //{
        //    // 1. Get students of course+branch
        //    var students = _context.Students
        //        .Where(s => s.TenantId == tenantId && s.BranchId == branchId && s.CourseId == courseId && !s.IsDeleted)
        //        .OrderBy(s => s.Id)
        //        .ToList();

        //    var studentIds = students.Select(s => s.Id).ToList();

        //    // 2. Get Daily Assessments for this timetable
        //    var dailyAssessments = _context.DailyAssessments
        //        .Where(d =>
        //            d.TenantId == tenantId &&
        //            d.BranchId == branchId &&
        //            d.TimeTableId == timeTableId &&
        //            !d.IsDeleted &&
        //            studentIds.Contains(d.StudentId))
        //        .Include(d => d.Grade)
        //        .Include(d => d.Assessment)
        //            .ThenInclude(a => a.AssessmentSkill)
        //        .ToList();

        //    // 3. Extract assessments present in this time table only
        //    var uniqueAssessments = dailyAssessments
        //        .Select(d => d.Assessment)
        //        .Where(a => a != null && !string.IsNullOrEmpty(a.Name))
        //        .GroupBy(a => a.Name)
        //        .ToDictionary(g => g.Key, g => g.First().Id);

        //    var headers = new List<string> { "S.NO.", "NAME OF THE STUDENT" };
        //    headers.AddRange(uniqueAssessments.Keys.OrderBy(name => name));

        //    // 4. Build Matrix Rows
        //    var rows = new List<AssessmentMatrixRow>();
        //    int serial = 1;

        //    foreach (var student in students)
        //    {
        //        var row = new AssessmentMatrixRow
        //        {
        //            SNo = serial++,
        //            StudentId = student.Id,
        //            Name = student.Name,
        //            Grades = new Dictionary<string, GradeDetail>()
        //        };

        //        foreach (var entry in uniqueAssessments)
        //        {
        //            var assessmentName = entry.Key;
        //            var assessmentId = entry.Value;

        //            var da = dailyAssessments.FirstOrDefault(d =>
        //                d.StudentId == student.Id &&
        //                d.AssessmentId == assessmentId);

        //            row.Grades[assessmentName] = new GradeDetail
        //            {
        //                Grade = da?.Grade?.Name ?? "Marks Not Added",
        //                Id = da.Id
        //            };
        //        }

        //        rows.Add(row);
        //    }

        //    return new AssessmentMatrixResponse
        //    {
        //        Headers = headers,
        //        Rows = rows
        //    };
        //}

        public UpdateGradeResponseVm UpdateStudentGrade(int id, int timeTableId, int studentId, int branchId, int newGradeId)
        {
            var entity = _context.DailyAssessments
                .FirstOrDefault(x =>
                    x.Id == id &&
                    x.TimeTableId == timeTableId &&
                    x.StudentId == studentId &&
                    x.BranchId == branchId &&
                    !x.IsDeleted);

            if (entity == null)
                return null;

            var originalGradeId = entity.GradeId ?? 0;

            entity.GradeId = newGradeId;
            entity.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();

            return new UpdateGradeResponseVm
            {
                Id = entity.Id,
                TimeTableId = entity.TimeTableId,
                StudentId = entity.StudentId,
                BranchId = entity.BranchId ?? 0,
                OriginalGradeId = originalGradeId,
                UpdatedGradeId = newGradeId
            };
        }

        public bool SaveAssessmentMatrix(SaveAssessmentMatrixRequestVm request)
        {
            try
            {
                var now = DateTime.UtcNow;

                // Step 1: Load existing records
                var existingMap = _context.DailyAssessments
                    .Where(x =>
                        !x.IsDeleted &&
                        x.TimeTableId == request.TimeTableId &&
                        x.BranchId == request.BranchId &&
                        x.TenantId == request.TenantId)
                    .ToDictionary(x => (x.StudentId, x.AssessmentId));

                Console.WriteLine($"[DEBUG] Existing assessments found: {existingMap.Count}");

                // Step 2: Insert or update grades
                foreach (var student in request.Students)
                {
                    Console.WriteLine($"[DEBUG] Processing student: {student.StudentId}");

                    foreach (var grade in student.Grades)
                    {
                        var key = (student.StudentId, grade.AssessmentId);

                        if (existingMap.TryGetValue(key, out var existing))
                        {
                            if (existing.GradeId != grade.GradeId)
                            {
                                existing.GradeId = grade.GradeId;
                                existing.UpdatedBy = request.ConductedById;
                                existing.UpdatedOn = now;
                                Console.WriteLine($"[DEBUG] Updated Grade: StudentId={student.StudentId}, AssessmentId={grade.AssessmentId}, GradeId={grade.GradeId}");
                            }
                        }
                        else
                        {
                            var newEntry = new MDailyAssessment
                            {
                                AssessmentDate = now,
                                TimeTableId = request.TimeTableId,
                                StudentId = student.StudentId,
                                AssessmentId = grade.AssessmentId,
                                GradeId = grade.GradeId,
                                ConductedById = request.ConductedById,
                                BranchId = request.BranchId,
                                TenantId = request.TenantId,
                                CreatedBy = request.ConductedById,
                                CreatedOn = now,
                                IsDeleted = false
                            };

                            _context.DailyAssessments.Add(newEntry);

                            Console.WriteLine($"[DEBUG] Added Grade: StudentId={student.StudentId}, AssessmentId={grade.AssessmentId}, GradeId={grade.GradeId}");
                        }
                    }
                }

                // Log number of new additions before saving
                var addedCount = _context.ChangeTracker.Entries()
                    .Count(e => e.State == EntityState.Added);
                Console.WriteLine($"[DEBUG] Total new records to insert: {addedCount}");

                var saveResult = _context.SaveChanges();
                Console.WriteLine($"[DEBUG] SaveChanges() result: {saveResult}");

                // Step 3: Update timetable assessment status
                var timeTable = _context.TimeTables.FirstOrDefault(tt =>
                    !tt.IsDeleted &&
                    tt.Id == request.TimeTableId &&
                    tt.TenantId == request.TenantId);

                if (timeTable != null)
                {
                    timeTable.AssessmentStatusCode = request.OverrideStatusCode;
                    timeTable.UpdatedBy = request.ConductedById;
                    timeTable.UpdatedOn = now;

                    var updateResult = _context.SaveChanges();
                    Console.WriteLine($"[DEBUG] TimeTable status updated. SaveChanges() result: {updateResult}");
                }
                else
                {
                    Console.WriteLine($"[DEBUG] TimeTable not found for ID: {request.TimeTableId}");
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] SaveAssessmentMatrix Exception: {ex}");
                return false;
            }
        }





        public DailyAssessmentPerformanceSummaryResponse GetPerformanceSummary(int tenantId, int courseId, int branchId, int weekId)
        {
            // Step 1: Fetch students for the given tenant, course, and branch
            var students = _context.Students
                .Where(s => !s.IsDeleted && s.TenantId == tenantId && s.BranchId == branchId && s.CourseId == courseId)
                .Select(s => new StudentInfoVm
                {
                    StudentId = s.Id,
                    StudentName = s.Name
                })
                .OrderBy(s => s.StudentId)
                .ToList();

            var studentIds = students.Select(s => s.StudentId).ToList();

            // Step 2: Fetch daily assessments for the selected week
            var dailyAssessments = _context.DailyAssessments
                .Where(d =>
                    !d.IsDeleted &&
                    d.TenantId == tenantId &&
                    d.BranchId == branchId &&
                    studentIds.Contains(d.StudentId) &&
                    d.TimeTable.WeekId == weekId)
                .Include(d => d.Grade)
                .Include(d => d.Assessment)
                .ThenInclude(a => a.AssessmentSkill)
                .ToList();

            // Step 3: Build unique assessment headers
            var headers = dailyAssessments
                .Where(d => d.Assessment != null)
                .Select(d => d.Assessment.Name)
                .Distinct()
                .OrderBy(h => h)
                .ToList();

            // Step 4: Build unique grade names used
            var gradesUsed = dailyAssessments
                .Select(d => d.Grade?.Name ?? "Not Graded")
                .Distinct()
                .ToList();

            // Step 5: Build matrix of assessment scores
            var assessmentGrades = new Dictionary<string, Dictionary<int, AssessmentScoreVm>>();
            var studentScores = new Dictionary<int, List<decimal>>();

            foreach (var header in headers)
            {
                assessmentGrades[header] = new Dictionary<int, AssessmentScoreVm>();

                foreach (var student in students)
                {
                    var da = dailyAssessments.FirstOrDefault(d =>
                        d.Assessment.Name == header &&
                        d.StudentId == student.StudentId);

                    var gradeName = da?.Grade?.Name ?? "Not Graded";
                    decimal? score = da?.Grade?.MaxPercentage;
                    DateTime? assessmentDate = da?.AssessmentDate;

                    assessmentGrades[header][student.StudentId] = new AssessmentScoreVm
                    {
                        Grade = gradeName,
                        Score = score,
                        AssessmentDate = assessmentDate
                    };

                    if (score.HasValue)
                    {
                        if (!studentScores.ContainsKey(student.StudentId))
                            studentScores[student.StudentId] = new List<decimal>();

                        studentScores[student.StudentId].Add(score.Value);
                    }
                }
            }

            // Step 6: Compute average and std. deviation for each student
            foreach (var student in students)
            {
                if (studentScores.TryGetValue(student.StudentId, out var scores) && scores.Any())
                {
                    var avg = scores.Average();
                    var variance = scores.Sum(s => (decimal)Math.Pow((double)(s - avg), 2)) / scores.Count;
                    var stdDev = Math.Sqrt((double)variance);

                    student.AverageScore = Math.Round(avg, 2);
                    student.StandardDeviation = Math.Round((decimal)stdDev, 2);
                }
            }

            // Step 7: Return final response
            return new DailyAssessmentPerformanceSummaryResponse
            {
                Headers = headers,
                Students = students,
                AssessmentGrades = assessmentGrades,
                AssessmentStatusCode = gradesUsed
            };
        }



    }
}
