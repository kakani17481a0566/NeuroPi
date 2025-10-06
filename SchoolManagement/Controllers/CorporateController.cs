using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Corporate;
using SchoolManagement.Response;
using System.Net;
using System.Collections.Generic;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CorporateController : ControllerBase
    {
        private readonly ICorporateService _corporateService;

        public CorporateController(ICorporateService corporateService)
        {
            _corporateService = corporateService;
        }

        [HttpGet("{tenantId}")]
        public ResponseResult<List<CorporateVM>> GetAll(int tenantId)
        {
            var corporates = _corporateService.GetAll(tenantId);

            if (corporates.Count == 0)
            {
                return new ResponseResult<List<CorporateVM>>(
                    HttpStatusCode.NotFound,
                    corporates,
                    "No corporates found for this tenant."
                );
            }

            return new ResponseResult<List<CorporateVM>>(
                HttpStatusCode.OK,
                corporates,
                "Corporate list retrieved successfully."
            );
        }
    }
}
