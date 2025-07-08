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

                // Step 2: Insert or update grades
                foreach (var student in request.Students)
                {
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
                            }
                        }
                        else
                        {
                            _context.DailyAssessments.Add(new MDailyAssessment
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
                            });
                        }
                    }
                }

                _context.SaveChanges();

                // Step 3: Update timetable assessment status if override provided
                var timeTable = _context.TimeTables.FirstOrDefault(tt =>
                    !tt.IsDeleted &&
                    tt.Id == request.TimeTableId &&
                    tt.TenantId == request.TenantId);

                if (timeTable != null)
                {
                    timeTable.AssessmentStatusCode = request.OverrideStatusCode;
                    timeTable.UpdatedBy = request.ConductedById;
                    timeTable.UpdatedOn = now;
                    _context.SaveChanges();

                    Console.WriteLine($"[DEBUG] TimeTable status updated to: {request.OverrideStatusCode}");
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



    }
}
