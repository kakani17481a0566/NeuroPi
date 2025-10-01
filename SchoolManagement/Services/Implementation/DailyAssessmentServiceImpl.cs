using Azure.Core;
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
            var model = request.ToModel(request);
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
            // 1. Get students
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

            // 2. Resolve week
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            int? effectiveWeekId = weekId switch
            {
                -1 => _context.Weeks
                        .Where(w => !w.IsDeleted && w.StartDate <= today && w.EndDate >= today)
                        .Select(w => (int?)w.Id)
                        .FirstOrDefault(),
                0 => null,
                > 0 => weekId,
                _ => null
            };

            // 3. Assessments query
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

            // 4. Week dictionary
            var weekDictionary = _context.Weeks
                .Where(w => !w.IsDeleted)
                .OrderBy(w => w.StartDate)
                .ToDictionary(
                    w => w.Id,
                    w => $"{w.Name} ({w.StartDate:dd MMM} - {w.EndDate:dd MMM})");

            // 5. Selected week
            var selectedWeek = effectiveWeekId.HasValue
                ? dailyAssessments.Select(d => d.TimeTable.Week).FirstOrDefault(w => w.Id == effectiveWeekId)
                : dailyAssessments.Select(d => d.TimeTable.Week).OrderBy(w => w.StartDate).FirstOrDefault();

            string? weekName = selectedWeek?.Name;
            DateOnly? weekStartDate = selectedWeek?.StartDate;
            DateOnly? weekEndDate = selectedWeek?.EndDate;

            // 6. Header details
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

            // Names for UI
            var headers = headerDetails.Select(h => h.Name).ToList();

            var gradesUsed = dailyAssessments
                .Select(d => d.Grade?.Name ?? "Not Graded")
                .Distinct()
                .ToList();

            // 7. AssessmentGrades keyed by AssessmentId
            var assessmentGrades = new Dictionary<int, Dictionary<int, AssessmentScoreVm>>();
            var studentScores = new Dictionary<int, List<decimal>>();

            foreach (var header in headerDetails)
            {
                assessmentGrades[header.AssessmentId] = new Dictionary<int, AssessmentScoreVm>();

                foreach (var student in students)
                {
                    var da = dailyAssessments
                        .Where(d => d.Assessment.Id == header.AssessmentId && d.StudentId == student.StudentId)
                        .OrderByDescending(d => d.AssessmentDate)
                        .ThenByDescending(d => d.TimeTable.Id) // tie-breaker
                        .FirstOrDefault();

                    var gradeName = da?.Grade?.Name ?? "Not Graded";
                    decimal? score = da?.Grade?.MaxPercentage;
                    DateTime? assessmentDate = da?.AssessmentDate;

                    assessmentGrades[header.AssessmentId][student.StudentId] = new AssessmentScoreVm
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

            // 8. Student averages
            foreach (var student in students)
            {
                if (studentScores.TryGetValue(student.StudentId, out var scores) && scores.Any())
                {
                    var avg = scores.Average();
                    if (scores.Count > 1)
                    {
                        var variance = scores.Sum(s => (decimal)Math.Pow((double)(s - avg), 2)) / (scores.Count - 1);
                        var stdDev = Math.Sqrt((double)variance);

                        student.AverageScore = Math.Round(avg, 2);
                        student.StandardDeviation = Math.Round((decimal)stdDev, 2);
                    }
                    else
                    {
                        student.AverageScore = Math.Round(avg, 2);
                        student.StandardDeviation = 0;
                    }
                }
            }

            // 9. Subject-wise
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
                            var scoreVm = assessmentGrades.TryGetValue(header.AssessmentId, out var studentMap) &&
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

            // 10. Timetable schedule
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

            // 11. Response
            return new DailyAssessmentPerformanceSummaryResponse
            {
                Headers = headers, // list of names for UI
                Students = students,
                AssessmentGrades = assessmentGrades, // keyed by AssessmentId
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


        public StudentPerformanceDashboard GetStudentPerformanceDashboard(int studentId, int tenantId)
        {
            var student = _context.Students
                .Where(s => s.Id == studentId && s.TenantId == tenantId && !s.IsDeleted)
                .Select(s => new { s.Id, s.Name, s.StudentImageUrl })
                .FirstOrDefault();

            if (student == null) return null;

            var assessments = _context.DailyAssessments
                .Where(a => !a.IsDeleted && a.StudentId == studentId && a.TenantId == tenantId)
                .Include(a => a.Assessment).ThenInclude(x => x.AssessmentSkill).ThenInclude(x => x.Subject)
                .Include(a => a.Grade)
                .Include(a => a.TimeTable).ThenInclude(t => t.Week)
                .ToList();

            // DAILY
            var today = DateOnly.FromDateTime(DateTime.Today);
            var dailyResults = assessments
                .Where(a => DateOnly.FromDateTime(a.AssessmentDate) == today)
                .Select(a => new AssessmentResult
                {
                    AssessmentName = a.Assessment?.Name,
                    SkillName = a.Assessment?.AssessmentSkill?.Name,
                    SubjectName = a.Assessment?.AssessmentSkill?.Subject?.Name,
                    Grade = a.Grade?.Name ?? "N/A",
                    MinPercentage = a.Grade?.MinPercentage,
                    MaxPercentage = a.Grade?.MaxPercentage,
                    TimeTableDay = a.TimeTable?.Name,
                    AssessmentDate = a.AssessmentDate
                }).ToList();

            // WEEKLY – All Weeks
            var weekGroups = assessments
                .Where(a => a.TimeTable?.Week != null)
                .GroupBy(a => a.TimeTable.Week)
                .Select(g => new PerformanceBreakdown
                {
                    Period = $"{g.Key.Name} ({g.Key.StartDate:dd MMM} - {g.Key.EndDate:dd MMM})",
                    Results = g.Select(a => new AssessmentResult
                    {
                        AssessmentName = a.Assessment?.Name,
                        SkillName = a.Assessment?.AssessmentSkill?.Name,
                        SubjectName = a.Assessment?.AssessmentSkill?.Subject?.Name,
                        Grade = a.Grade?.Name ?? "N/A",
                        MinPercentage = a.Grade?.MinPercentage,
                        MaxPercentage = a.Grade?.MaxPercentage,
                        TimeTableDay = a.TimeTable?.Name,
                        AssessmentDate = a.AssessmentDate
                    }).ToList()
                }).ToList();

            // MONTHLY
            var currentMonth = DateTime.Today.Month;
            var currentYear = DateTime.Today.Year;
            var monthlyResults = assessments
                .Where(a => a.AssessmentDate.Month == currentMonth && a.AssessmentDate.Year == currentYear)
                .Select(a => new AssessmentResult
                {
                    AssessmentName = a.Assessment?.Name,
                    SkillName = a.Assessment?.AssessmentSkill?.Name,
                    SubjectName = a.Assessment?.AssessmentSkill?.Subject?.Name,
                    Grade = a.Grade?.Name ?? "N/A",
                    MinPercentage = a.Grade?.MinPercentage,
                    MaxPercentage = a.Grade?.MaxPercentage,
                    TimeTableDay = a.TimeTable?.Name,
                    AssessmentDate = a.AssessmentDate
                }).ToList();

            // YEARLY
            var yearlyResults = assessments
                .Where(a => a.AssessmentDate.Year == currentYear)
                .Select(a => new AssessmentResult
                {
                    AssessmentName = a.Assessment?.Name,
                    SkillName = a.Assessment?.AssessmentSkill?.Name,
                    SubjectName = a.Assessment?.AssessmentSkill?.Subject?.Name,
                    Grade = a.Grade?.Name ?? "N/A",
                    MinPercentage = a.Grade?.MinPercentage,
                    MaxPercentage = a.Grade?.MaxPercentage,
                    TimeTableDay = a.TimeTable?.Name,
                    AssessmentDate = a.AssessmentDate
                }).ToList();

            // GROUP BY ASSESSMENTS
            var groupedAssessments = assessments
                .Where(a => a.Assessment != null)
                .GroupBy(a => a.Assessment.Id)
                .Select(g => new AssessmentGroup
                
                {
                    AssessmentName = g.First().Assessment?.Name,
                    SkillName = g.First().Assessment?.AssessmentSkill?.Name,
                    SubjectName = g.First().Assessment?.AssessmentSkill?.Subject?.Name,
                    Grade = g.First().Grade?.Name ?? "N/A",
                    MinPercentage = g.First().Grade?.MinPercentage,
                    MaxPercentage = g.First().Grade?.MaxPercentage,
                    TimeTableDay = g.First().TimeTable?.Name,
                    AssessmentDate = g.First().AssessmentDate
                }).ToList();

            return new StudentPerformanceDashboard
            {
                StudentId = student.Id,
                StudentName = student.Name,
                ImageUrl = student.StudentImageUrl,

                Daily = new PerformanceBreakdown
                {
                    Period = today.ToString("yyyy-MM-dd"),
                    Results = dailyResults
                },
                Weekly = null, // If needed, you can assign the first or current week here
                Monthly = new PerformanceBreakdown
                {
                    Period = $"{DateTime.Today:MMMM yyyy}",
                    Results = monthlyResults
                },
                Yearly = new PerformanceBreakdown
                {
                    Period = $"{DateTime.Today:yyyy}",
                    Results = yearlyResults
                },

                AssessmentWise = groupedAssessments
            };
        }



        public DailyAssessmentPerformanceSummaryResponse GetPerformanceSummaryForStudent(
       int tenantId,
       int courseId,
       int branchId,
       int weekId,
       int studentId)
        {
            // 1. Fetch student
            var student = _context.Students
                .AsNoTracking()
                .Where(s => !s.IsDeleted &&
                            s.TenantId == tenantId &&
                            s.BranchId == branchId &&
                            s.CourseId == courseId &&
                            s.Id == studentId)
                .Select(s => new StudentInfoVm
                {
                    StudentId = s.Id,
                    StudentName = s.Name
                })
                .FirstOrDefault();

            if (student == null)
            {
                return new DailyAssessmentPerformanceSummaryResponse();
            }

            // 2. Resolve week
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            int? effectiveWeekId = weekId switch
            {
                -1 => _context.Weeks
                        .AsNoTracking()
                        .Where(w => !w.IsDeleted && w.StartDate <= today && w.EndDate >= today)
                        .Select(w => (int?)w.Id)
                        .FirstOrDefault(),
                0 => null,
                > 0 => weekId,
                _ => null
            };

            // 3. Daily assessments
            var dailyAssessments = _context.DailyAssessments
                .AsNoTracking()
                .Where(d => !d.IsDeleted &&
                            d.TenantId == tenantId &&
                            d.BranchId == branchId &&
                            d.StudentId == studentId &&
                            (effectiveWeekId == null || d.TimeTable.WeekId == effectiveWeekId))
                .Select(d => new
                {
                    d.StudentId,
                    d.AssessmentId,
                    AssessmentName = d.Assessment.Name,
                    SkillId = d.Assessment.AssessmentSkill.Id,
                    SkillName = d.Assessment.AssessmentSkill.Name,
                    SubjectId = d.Assessment.AssessmentSkill.Subject.Id,
                    SubjectName = d.Assessment.AssessmentSkill.Subject.Name,
                    SubjectCode = d.Assessment.AssessmentSkill.Subject.Code,
                    GradeName = d.Grade.Name,
                    MinPercentage = (decimal?)d.Grade.MinPercentage,
                    MaxPercentage = (decimal?)d.Grade.MaxPercentage,
                    d.AssessmentDate,
                    TimeTableId = d.TimeTable.Id,
                    TimeTableName = d.TimeTable.Name,
                    Date = d.TimeTable.Date,
                    WeekId = d.TimeTable.Week.Id,
                    WeekName = d.TimeTable.Week.Name,
                    WeekStart = d.TimeTable.Week.StartDate,
                    WeekEnd = d.TimeTable.Week.EndDate,
                    TermId = d.TimeTable.Week.Term.Id,
                    TermName = d.TimeTable.Week.Term.Name,
                    TermStart = d.TimeTable.Week.Term.StartDate,
                    TermEnd = d.TimeTable.Week.Term.EndDate
                })
                .ToList();

            // 4. Week dictionary
            var weekDictionary = _context.Weeks
                .AsNoTracking()
                .Where(w => !w.IsDeleted)
                .OrderBy(w => w.StartDate)
                .ToDictionary(
                    w => w.Id,
                    w => $"{w.Name} ({w.StartDate:dd MMM} - {w.EndDate:dd MMM})");

            // 5. Header details
            var headerDetails = dailyAssessments
                .GroupBy(d => d.AssessmentId)
                .Select(g => new AssessmentHeaderVm
                {
                    AssessmentId = g.Key,
                    Name = g.First().AssessmentName,
                    SkillId = g.First().SkillId,
                    SkillName = g.First().SkillName,
                    SubjectId = g.First().SubjectId,
                    SubjectName = g.First().SubjectName,
                    SubjectCode = g.First().SubjectCode
                })
                .OrderBy(h => h.AssessmentId)
                .ToList();

            var headers = headerDetails.Select(h => h.AssessmentId).ToList();
            var gradesUsed = dailyAssessments
                .Select(d => d.GradeName ?? "Not Graded")
                .Distinct()
                .ToList();

            // 6. Latest score per assessment
            var assessmentGrades = headers.ToDictionary(
                id => id,
                id => new Dictionary<int, AssessmentScoreVm>
                {
                    [student.StudentId] = dailyAssessments
                        .Where(d => d.AssessmentId == id)
                        .OrderByDescending(d => d.AssessmentDate)
                        .ThenByDescending(d => d.TimeTableId)
                        .Select(d => new AssessmentScoreVm
                        {
                            Grade = d.GradeName ?? "Not Graded",
                            Score = (d.MinPercentage.HasValue && d.MaxPercentage.HasValue)
                                ? Math.Round((d.MinPercentage.Value + d.MaxPercentage.Value) / 2m, 2)
                                : (decimal?)null,
                            AssessmentDate = d.AssessmentDate
                        })
                        .FirstOrDefault() ?? new AssessmentScoreVm()
                });

            // 7. Overall avg & std dev
            var scores = dailyAssessments
                .Where(d => d.MinPercentage.HasValue && d.MaxPercentage.HasValue)
                .Select(d => (d.MinPercentage.Value + d.MaxPercentage.Value) / 2m)
                .ToList();

            if (scores.Count > 1)
            {
                var avg = scores.Average();
                var variance = scores.Sum(s => (s - avg) * (s - avg)) / (scores.Count - 1);
                var stdDev = Math.Sqrt((double)variance);

                student.AverageScore = Math.Round(avg, 2);
                student.StandardDeviation = Math.Round((decimal)stdDev, 2);
            }
            else if (scores.Count == 1)
            {
                student.AverageScore = Math.Round(scores.First(), 2);
                student.StandardDeviation = 0;
            }

            // 8. Subject-wise overall
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
                        StudentScores = new List<StudentScoreEntryVm>
                        {
                    assessmentGrades.TryGetValue(header.AssessmentId, out var map) &&
                    map.TryGetValue(student.StudentId, out var score)
                        ? new StudentScoreEntryVm
                        {
                            StudentId = student.StudentId,
                            Grade = score.Grade,
                            Score = score.Score,
                            AssessmentDate = score.AssessmentDate
                        }
                        : new StudentScoreEntryVm { StudentId = student.StudentId }
                        }
                    }).ToList()
                }).ToList();

            // 9. Timetable schedule
            var assessmentSchedule = dailyAssessments
                .GroupBy(d => d.TimeTableId)
                .Select(g => new AssessmentScheduleVm
                {
                    TimeTableId = g.Key,
                    Name = g.First().TimeTableName,
                    Date = DateOnly.FromDateTime(g.First().Date)
                })
                .OrderBy(s => s.Date)
                .ToList();

            // 10. Weekly analysis
            var weeklyAnalysis = dailyAssessments
                .GroupBy(d => new { d.WeekId, d.WeekName, d.WeekStart, d.WeekEnd })
                .Select(g =>
                {
                    var weekScores = g
                        .Where(x => x.MinPercentage.HasValue && x.MaxPercentage.HasValue)
                        .Select(x => (x.MinPercentage.Value + x.MaxPercentage.Value) / 2m)
                        .ToList();

                    decimal? avg = null, stdDev = null;
                    if (weekScores.Count > 1)
                    {
                        avg = Math.Round(weekScores.Average(), 2);
                        var variance = weekScores.Sum(s => (s - avg.Value) * (s - avg.Value)) / (weekScores.Count - 1);
                        stdDev = Math.Round((decimal)Math.Sqrt((double)variance), 2);
                    }
                    else if (weekScores.Count == 1)
                    {
                        avg = Math.Round(weekScores.First(), 2);
                        stdDev = 0;
                    }

                    var subjectWise = g.GroupBy(h => new { h.SubjectId, h.SubjectName, h.SubjectCode })
                        .Select(subjectGroup =>
                        {
                            var subjectScores = subjectGroup
                                .Where(x => x.MinPercentage.HasValue && x.MaxPercentage.HasValue)
                                .Select(x => (x.MinPercentage.Value + x.MaxPercentage.Value) / 2m)
                                .ToList();

                            decimal? subjAvg = null;
                            if (subjectScores.Count > 0)
                                subjAvg = Math.Round(subjectScores.Average(), 2);

                            return new SubjectGroupedAssessmentVm
                            {
                                SubjectId = subjectGroup.Key.SubjectId,
                                SubjectName = subjectGroup.Key.SubjectName,
                                SubjectCode = subjectGroup.Key.SubjectCode,
                                AverageScore = subjAvg,
                                Skills = new List<SkillAssessmentVm>()
                            };
                        })
                        .ToList();

                    return new WeeklyPerformanceVm
                    {
                        WeekId = g.Key.WeekId,
                        WeekName = g.Key.WeekName,
                        StartDate = g.Key.WeekStart,
                        EndDate = g.Key.WeekEnd,
                        AverageScore = avg,
                        StandardDeviation = stdDev,
                        SubjectWiseAssessments = subjectWise
                    };
                })
                .OrderBy(w => w.StartDate)
                .ToList();

            // 11. Term analysis
            var grades = _context.Grades
                .AsNoTracking()
                .Where(g => !g.IsDeleted)
                .Select(g => new { g.Name, g.MinPercentage, g.MaxPercentage })
                .ToList();

            var termAnalysis = dailyAssessments
                .GroupBy(d => new { d.TermId, d.TermName, d.TermStart, d.TermEnd })
                .Select(g =>
                {
                    var termScores = g
                        .Where(x => x.MinPercentage.HasValue && x.MaxPercentage.HasValue)
                        .Select(x => (x.MinPercentage.Value + x.MaxPercentage.Value) / 2m)
                        .ToList();

                    decimal? avg = null, stdDev = null;
                    if (termScores.Count > 1)
                    {
                        avg = Math.Round(termScores.Average(), 2);
                        var variance = termScores.Sum(s => (s - avg.Value) * (s - avg.Value)) / (termScores.Count - 1);
                        stdDev = Math.Round((decimal)Math.Sqrt((double)variance), 2);
                    }
                    else if (termScores.Count == 1)
                    {
                        avg = Math.Round(termScores.First(), 2);
                        stdDev = 0;
                    }

                    // Term-level grade
                    string termGrade = "Not Graded";
                    if (avg.HasValue)
                    {
                        var rounded = Math.Round(avg.Value, 0);
                        var match = grades.FirstOrDefault(gr =>
                            gr.MinPercentage <= rounded && gr.MaxPercentage >= rounded);
                        termGrade = match?.Name ?? "Not Graded";
                    }

                    var subjectWise = g.GroupBy(h => new { h.SubjectId, h.SubjectName, h.SubjectCode })
                        .Select(subjectGroup =>
                        {
                            var subjectScores = subjectGroup
                                .Where(x => x.MinPercentage.HasValue && x.MaxPercentage.HasValue)
                                .Select(x => (x.MinPercentage.Value + x.MaxPercentage.Value) / 2m)
                                .ToList();

                            decimal? subjAvg = null;
                            string subjGrade = "Not Graded";
                            if (subjectScores.Count > 0)
                            {
                                subjAvg = Math.Round(subjectScores.Average(), 2);
                                var rounded = Math.Round(subjAvg.Value, 0);
                                var match = grades.FirstOrDefault(gr =>
                                    gr.MinPercentage <= rounded && gr.MaxPercentage >= rounded);
                                subjGrade = match?.Name ?? "Not Graded";
                            }

                            return new SubjectGroupedAssessmentVm
                            {
                                SubjectId = subjectGroup.Key.SubjectId,
                                SubjectName = subjectGroup.Key.SubjectName,
                                SubjectCode = subjectGroup.Key.SubjectCode,
                                AverageScore = subjAvg,
                                Grade = subjGrade,
                                Skills = new List<SkillAssessmentVm>()
                            };
                        })
                        .ToList();

                    return new TermPerformanceVm
                    {
                        TermId = g.Key.TermId,
                        TermName = g.Key.TermName,
                        StartDate = DateOnly.FromDateTime(g.Key.TermStart),
                        EndDate = DateOnly.FromDateTime(g.Key.TermEnd),
                        AverageScore = avg,
                        StandardDeviation = stdDev,
                        Grade = termGrade,
                        SubjectWiseAssessments = subjectWise
                    };
                })
                .OrderBy(t => t.StartDate)
                .ToList();

            var termDictionary = termAnalysis.ToDictionary(
                t => t.TermId,
                t => $"{t.TermName} ({t.StartDate:dd MMM} - {t.EndDate:dd MMM})");

            // 12. Pick current week details
            var selectedWeek = effectiveWeekId.HasValue
                ? dailyAssessments.FirstOrDefault(d => d.WeekId == effectiveWeekId.Value)
                : dailyAssessments.FirstOrDefault();

            string? weekName = selectedWeek?.WeekName;
            DateOnly? weekStartDate = selectedWeek?.WeekStart;
            DateOnly? weekEndDate = selectedWeek?.WeekEnd;

            // 13. Final response
            return new DailyAssessmentPerformanceSummaryResponse
            {
                Headers = headerDetails.Select(h => h.Name).ToList(),
                Students = new List<StudentInfoVm> { student },
                AssessmentGrades = assessmentGrades,
                AssessmentStatusCode = gradesUsed,
                HeaderDetails = headerDetails,
                SubjectWiseAssessments = subjectWiseAssessments,
                StudentDictionary = new Dictionary<int, string> { [student.StudentId] = student.StudentName },
                SubjectDictionary = subjectWiseAssessments
                    .GroupBy(s => s.SubjectId)
                    .ToDictionary(g => g.Key, g => g.First().SubjectName),
                WeekDictionary = weekDictionary,
                TermDictionary = termDictionary,
                WeekName = weekName,
                WeekStartDate = weekStartDate,
                WeekEndDate = weekEndDate,
                AssessmentSchedule = assessmentSchedule,
                WeeklyAnalysis = weeklyAnalysis,
                TermAnalysis = termAnalysis
            };
        }




    }
}
