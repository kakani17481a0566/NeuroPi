﻿using SchoolManagement.ViewModel.Student;
using SchoolManagement.ViewModel.Students;
using SchoolManagement.ViewModel.Subject;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface IStudentService
    {
        List<StudentResponseVM> GetAll();
        StudentResponseVM GetById(int id);
        List<StudentResponseVM> GetAllByTenantId(int tenantId);
        StudentResponseVM GetByIdAndTenantId(int id, int tenantId);
        List<StudentResponseVM> GetByTenantAndBranch(int tenantId, int branchId); // ✅ Add this
        StudentResponseVM Create(StudentRequestVM request); // ✅ Add these if not present
        StudentResponseVM Update(int id, StudentRequestVM request);
        StudentResponseVM Delete(int id);

        StudentVM GetByTenantCourseBranch(int tenantId, int courseId, int branchId);
        StudentsData GetStudentDetails(int courseId, int branchId,DateOnly date, int tenantId);

        List<VStudentPerformanceVM> GetStudentPerformance(int tenantId, int courseId, int branchId);

        VStudentPerformanceChartVM GetStudentPerformanceChartData(int tenantId, int courseId, int branchId);


    }
}
