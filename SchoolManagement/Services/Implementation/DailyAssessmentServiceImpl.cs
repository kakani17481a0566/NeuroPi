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

            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            int? effectiveWeekId = weekId switch
            {
                -1 => _context.Weeks
                        .Where(w => !w.IsDeleted && w.StartDate <= today && w.EndDate >= today)
                        .Select(w => (int?)w.Id)
                        .FirstOrDefault(),
                0 => null, // Fetch all weeks
                > 0 => weekId,
                _ => null
            };

            var dailyAssessmentsQuery = _context.DailyAssessments
                .Where(d => !d.IsDeleted && d.TenantId == tenantId && d.BranchId == branchId && studentIds.Contains(d.StudentId));

            if (effectiveWeekId != null)
            {
                dailyAssessmentsQuery = dailyAssessmentsQuery.Where(d => d.TimeTable.WeekId == effectiveWeekId);
            }

            var dailyAssessments = dailyAssessmentsQuery
                .Include(d => d.Grade)
                .Include(d => d.Assessment).ThenInclude(a => a.AssessmentSkill).ThenInclude(s => s.Subject)
                .Include(d => d.TimeTable).ThenInclude(t => t.Week)
                .ToList();

            var weekDictionary = _context.Weeks
                .Where(w => !w.IsDeleted)
                .OrderBy(w => w.StartDate)
                .ToDictionary(
                    w => w.Id,
                    w => $"{w.Name} ({w.StartDate:dd MMM} - {w.EndDate:dd MMM})");

            var selectedWeek = dailyAssessments
                .Select(d => d.TimeTable.Week)
                .Where(w => w != null)
                .OrderBy(w => w.StartDate)
                .FirstOrDefault();

            string? weekName = selectedWeek?.Name;
            DateOnly? weekStartDate = selectedWeek?.StartDate;
            DateOnly? weekEndDate = selectedWeek?.EndDate;

            var headerDetails = dailyAssessments
                .Where(d => d.Assessment?.AssessmentSkill?.Subject != null)
                .GroupBy(d => d.Assessment.Id)
                .Select(g => new AssessmentHeaderVm
                {
                    AssessmentId = g.Key,
                    Name = g.First().Assessment.Name,
                    SkillId = g.First().Assessment.AssessmentSkill.Id,
                    SkillName = g.First().Assessment.AssessmentSkill.Name,
                    SubjectId = g.First().Assessment.AssessmentSkill.Subject.Id,
                    SubjectName = g.First().Assessment.AssessmentSkill.Subject.Name,
                    SubjectCode = g.First().Assessment.AssessmentSkill.Subject.Code
                })
                .OrderBy(h => h.AssessmentId)
                .ToList();

            var headers = headerDetails.Select(h => h.Name).ToList();
            var gradesUsed = dailyAssessments.Select(d => d.Grade?.Name ?? "Not Graded").Distinct().ToList();

            var assessmentGrades = new Dictionary<string, Dictionary<int, AssessmentScoreVm>>();
            var studentScores = new Dictionary<int, List<decimal>>();

            foreach (var header in headers)
            {
                assessmentGrades[header] = new Dictionary<int, AssessmentScoreVm>();

                foreach (var student in students)
                {
                    var da = dailyAssessments
                        .Where(d => d.Assessment.Name == header && d.StudentId == student.StudentId)
                        .OrderByDescending(d => d.AssessmentDate)
                        .FirstOrDefault();

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

            var subjectWiseAssessments = headerDetails
                .GroupBy(h => new { h.SubjectId, h.SubjectName, h.SubjectCode })
                .Select(subjectGroup => new SubjectGroupedAssessmentVm
                {
                    SubjectId = subjectGroup.Key.SubjectId,
                    SubjectName = subjectGroup.Key.SubjectName,
                    SubjectCode = subjectGroup.Key.SubjectCode,
                    Skills = subjectGroup.Select(header => new SkillAssessmentVm
                    {
                        AssessmentId = header.AssessmentId,
                        Name = header.Name,
                        SkillId = header.SkillId,
                        SkillName = header.SkillName,
                        StudentScores = students.Select(s =>
                        {
                            var scoreVm = assessmentGrades.TryGetValue(header.Name, out var studentMap) &&
                                          studentMap.TryGetValue(s.StudentId, out var score)
                                ? score
                                : new AssessmentScoreVm();

                            return new StudentScoreEntryVm
                            {
                                StudentId = s.StudentId,
                                Grade = scoreVm.Grade,
                                Score = scoreVm.Score,
                                AssessmentDate = scoreVm.AssessmentDate
                            };
                        }).ToList()
                    }).ToList()
                }).ToList();

            var assessmentSchedule = dailyAssessments
                .Select(d => d.TimeTable)
                .Where(t => t != null)
                .GroupBy(t => t.Id)
                .Select(g => new AssessmentScheduleVm
                {
                    TimeTableId = g.Key,
                    Name = g.First().Name,
                    Date = DateOnly.FromDateTime(g.First().Date)
                })
                .OrderBy(s => s.Date)
                .ToList();

            return new DailyAssessmentPerformanceSummaryResponse
            {
                Headers = headers,
                Students = students,
                AssessmentGrades = assessmentGrades,
                AssessmentStatusCode = gradesUsed,
                HeaderDetails = headerDetails,
                SubjectWiseAssessments = subjectWiseAssessments,
                StudentDictionary = students.ToDictionary(s => s.StudentId, s => s.StudentName),
                SubjectDictionary = subjectWiseAssessments.ToDictionary(s => s.SubjectId, s => s.SubjectName),
                WeekDictionary = weekDictionary,
                WeekName = weekName,
                WeekStartDate = weekStartDate,
                WeekEndDate = weekEndDate,
                AssessmentSchedule = assessmentSchedule
            };
        }





    }
}
