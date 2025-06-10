using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.VTimeTable;
using System.Collections.Generic;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VTermTableController : ControllerBase
    {
        private readonly IMVTermTableService _termTableService;

        public VTermTableController(IMVTermTableService termTableService)
        {
            _termTableService = termTableService;
        }

        [HttpGet("get-by-tenant-course-term")]
        public ResponseResult<List<MVTermTableVM>> GetByTenantCourseTerm(int tenantId, int courseId, int termId)
        {
            var result = _termTableService.GetVTermTableData(tenantId, courseId, termId);
            if (result != null) 
            {
                return new ResponseResult<List<MVTermTableVM>>(HttpStatusCode.OK, result, "Data retrieved successfully.");
            }
            else
            {
                return new ResponseResult<List<MVTermTableVM>>(HttpStatusCode.NotFound, null, "No data found for the provided parameters.");
            }
            

        }

        [HttpGet("get-week-matrix")]
        public ResponseResult<MVTermTableWeeklyMatrixVM> GetWeeklyMatrix(int tenantId, int courseId, int termId)
        {
            var result = _termTableService.GetTermTableWeeklyMatrix(tenantId, courseId, termId);

            if (result != null)
                return new ResponseResult<MVTermTableWeeklyMatrixVM>(HttpStatusCode.OK, result, "Weekly matrix fetched.");
            else
                return new ResponseResult<MVTermTableWeeklyMatrixVM>(HttpStatusCode.NotFound, null, "No data found.");
        }
    }
}
