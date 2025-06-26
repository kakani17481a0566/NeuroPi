using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Student;
using SchoolManagement.ViewModel.Students;

namespace SchoolManagement.Services.Implementation
{
    public class StudentServiceImpl : IStudentService
    {
        private readonly SchoolManagementDb _context;

        public StudentServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }

        public List<StudentResponseVM> GetAll()
        {
            return _context.Students
                .Where(s => !s.IsDeleted)
                .Select(StudentResponseVM.ToViewModel)
                .ToList();
        }

        public StudentResponseVM GetById(int id)
        {
            var student = _context.Students
                .FirstOrDefault(s => s.Id == id && !s.IsDeleted);
            return student == null ? null : StudentResponseVM.ToViewModel(student);
        }

        public List<StudentResponseVM> GetAllByTenantId(int tenantId)
        {
            return _context.Students
                .Where(s => s.TenantId == tenantId && !s.IsDeleted)
                .Select(StudentResponseVM.ToViewModel)
                .ToList();
        }

        public StudentResponseVM GetByIdAndTenantId(int id, int tenantId)
        {
            var student = _context.Students
                .FirstOrDefault(s => s.Id == id && s.TenantId == tenantId && !s.IsDeleted);
            return student == null ? null : StudentResponseVM.ToViewModel(student);
        }

        public List<StudentResponseVM> GetByTenantAndBranch(int tenantId, int branchId)
        {
            return _context.Students
                .Where(s => s.TenantId == tenantId && s.BranchId == branchId && !s.IsDeleted)
                .Select(StudentResponseVM.ToViewModel)
                .ToList();
        }

        public StudentResponseVM Create(StudentRequestVM request)
        {
            var newStudent = request.ToModel();
            newStudent.CreatedOn = DateTime.UtcNow;
            _context.Students.Add(newStudent);
            _context.SaveChanges();

            return StudentResponseVM.ToViewModel(newStudent);
        }

        public StudentResponseVM Update(int id, StudentRequestVM request)
        {
            var student = _context.Students.FirstOrDefault(s => s.Id == id && !s.IsDeleted);
            if (student == null) return null;

            student.Name = request.Name;
            student.CourseId = request.CourseId;
            student.BranchId = request.BranchId;
            student.TenantId = request.TenantId;
            student.UpdatedOn = DateTime.UtcNow;
            student.UpdatedBy = request.UpdatedBy;

            _context.SaveChanges();
            return StudentResponseVM.ToViewModel(student);
        }

        public StudentResponseVM Delete(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.Id == id && !s.IsDeleted);
            if (student == null) return null;

            student.IsDeleted = true;
            student.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();
            return StudentResponseVM.ToViewModel(student);
        }


       
     public StudentVM GetByTenantCourseBranch(int tenantId, int courseId, int branchId)
        {
            var result= _context.Students
                .Include(s => s.Course)
                .Include(s => s.Branch)
                .Where(s => s.TenantId == tenantId && s.CourseId == courseId && s.BranchId == branchId && !s.IsDeleted)
                .ToList();
            if(result!=null && result.Count > 0)
            {
                StudentVM student = new StudentVM();
                student.CourseId = result.First().CourseId;
                student.CourseName = result.First().Course.Name;
                student.BranchId = result.First().BranchId;
                student.BranchName = result.First().Branch.Name;
                student.TenantId = result.First().TenantId;
                List<Student> students = new List<Student>();
                foreach(MStudent stu in result){
                    Student s = new Student()
                    {
                        id = stu.Id,
                        name = stu.Name
                    };
                    students.Add(s);

                }
                student.students = students;
                return student;
            }
            return null;

        }


    }
}
