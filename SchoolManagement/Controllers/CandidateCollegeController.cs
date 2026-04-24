using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.CandidateCollege;
using System.Collections.Generic;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateCollegeController : ControllerBase
    {
        private readonly ICandidateCollegeService _candidateCollegeService;

        public CandidateCollegeController(ICandidateCollegeService candidateCollegeService)
        {
            _candidateCollegeService = candidateCollegeService;
        }

        [HttpGet("emp/{empId}/tenant/{tenantId}")]
        public ResponseResult<List<CandidateCollegeResponseVM>> GetByEmpIdAndTenantId(int empId, int tenantId)
        {
            var result = _candidateCollegeService.GetByEmpIdAndTenantId(empId, tenantId);
            if (result == null || result.Count == 0)
            {
                return new ResponseResult<List<CandidateCollegeResponseVM>>(System.Net.HttpStatusCode.NotFound, result, "Candidate College Not Found");
            }
            return new ResponseResult<List<CandidateCollegeResponseVM>>(System.Net.HttpStatusCode.OK, result, "Candidate College fetched successfully");
        }
    }
}