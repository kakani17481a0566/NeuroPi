using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.CandidateCollege;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManagement.Services.Implementation
{
    public class CandidateCollegeServiceImpl : ICandidateCollegeService
    {
        private readonly SchoolManagementDb _context;

        public CandidateCollegeServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }

        public List<CandidateCollegeResponseVM> GetByEmpIdAndTenantId(int empId, int tenantId)
        {
            var result = _context.CandidateColleges
                .Where(c => c.EmpId == empId && c.TenantId == tenantId && c.IsDeleted == false)
                .Include(c => c.College)
                .Include(c => c.Employee)
                .Select(c => new CandidateCollegeResponseVM
                {
                    Id = c.Id,
                    CollegeId = c.CollegeId,
                    CollegeName = c.College.CollegeName,
                    EmpId = c.EmpId,
                    EmployeeName = c.Employee.Name,
                    TenantId = c.TenantId,
                    CreatedOn = c.CreatedOn,
                    CreatedBy = c.CreatedBy,
                    UpdatedOn = c.UpdatedOn,
                    UpdatedBy = c.UpdatedBy,
                    IsDeleted = c.IsDeleted
                })
                .ToList();

            return result;
        }

        public List<CollegeDetailsVM> GetCollegeDetails(int empId, int tenantId)
        {
            var collegeDetails = _context.CandidateColleges
                .Where(c => c.EmpId == empId && c.TenantId == tenantId)
                .Select(c => new CollegeDetailsVM
                {
                     CollegeId = c.CollegeId,
                     CollegeName = c.College.CollegeName,
                     coursesList = _context.CollegeCourses
                            .Where(cc => cc.CollegeId == c.CollegeId)
                            .Select(cc => new Course
                            {
                                CourseId = cc.CourseId,
                                CourseName = cc.Course.CourseName
                            })
                                .ToList()
                }).ToList();
            return collegeDetails;
            
        }
    }
}