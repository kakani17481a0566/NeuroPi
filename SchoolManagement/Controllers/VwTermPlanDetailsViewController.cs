using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.VwTermPlanDetails;
using System.Collections.Generic;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VwTermPlanDetailsViewController : ControllerBase
    {
        private readonly IVwTermPlanDetailsService _termPlanService;

        public VwTermPlanDetailsViewController(IVwTermPlanDetailsService termPlanService)
        {
            _termPlanService = termPlanService;
        }

       
        [HttpGet("GetAll/{tenantId}")]
        public ResponseResult<List<VwTermPlanDetailsViewModel>> GetAllByTenantId(int tenantId)
        {
            var data = _termPlanService.GetAllByTenantId(tenantId);
            return new ResponseResult<List<VwTermPlanDetailsViewModel>>(
                HttpStatusCode.OK,
                data,
                "Term plan details fetched successfully."
            );
        }
    }
}
