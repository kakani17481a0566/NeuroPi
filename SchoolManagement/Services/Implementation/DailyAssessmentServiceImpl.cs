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
        public AssessmentMatrixResponse GetAssessmentMatrixByTimeTable(int tenantId, int courseId, int branchId, int timeTableId)
        {
            // 1. Get students of course+branch
            var students = _context.Students
                .Where(s => s.TenantId == tenantId && s.BranchId == branchId && s.CourseId == courseId && !s.IsDeleted)
                .OrderBy(s => s.Name)
                .ToList();

            var studentIds = students.Select(s => s.Id).ToList();

            // 2. Get Daily Assessments for this timetable
            var dailyAssessments = _context.DailyAssessments
                .Where(d =>
                    d.TenantId == tenantId &&
                    d.BranchId == branchId &&
                    d.TimeTableId == timeTableId &&
                    !d.IsDeleted &&
                    studentIds.Contains(d.StudentId))
                .Include(d => d.Grade)
                .Include(d => d.Assessment)
                    .ThenInclude(a => a.AssessmentSkill)
                .ToList();

            // 3. Extract assessments present in this time table only
            var uniqueAssessments = dailyAssessments
                .Select(d => d.Assessment)
                .Where(a => a != null && !string.IsNullOrEmpty(a.Name))
                .GroupBy(a => a.Name)
                .ToDictionary(g => g.Key, g => g.First().Id);

            var headers = new List<string> { "S.NO.", "NAME OF THE STUDENT" };
            headers.AddRange(uniqueAssessments.Keys.OrderBy(name => name));

            // 4. Build lookup: StudentId -> (AssessmentId -> GradeName)
            var gradeLookup = dailyAssessments
                .GroupBy(d => d.StudentId)
                .ToDictionary(
                    g => g.Key,
                    g => g.ToDictionary(x => x.AssessmentId, x => x.Grade?.Name ?? "Marks Not Added")
                );

            // 5. Build Matrix Rows
            var rows = new List<AssessmentMatrixRow>();
            int serial = 1;

            foreach (var student in students)
            {
                var row = new AssessmentMatrixRow
                {
                    SNo = serial++,
                    Name = student.Name,
                    Grades = new Dictionary<string, string>()
                };

                foreach (var entry in uniqueAssessments)
                {
                    var assessmentName = entry.Key;
                    var assessmentId = entry.Value;

                    string grade = "Marks Not Added";
                    if (gradeLookup.TryGetValue(student.Id, out var studentGrades))
                    {
                        if (studentGrades.TryGetValue(assessmentId, out var g))
                            grade = g;
                    }

                    row.Grades[assessmentName] = grade;
                }

                rows.Add(row);
            }

            return new AssessmentMatrixResponse
            {
                Headers = headers,
                Rows = rows
            };
        }


     




    }
}
