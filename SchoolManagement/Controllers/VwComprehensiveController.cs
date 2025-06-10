using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.VwComprehensiveTimeTables;
using System.Collections.Generic;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VwComprehensiveController : ControllerBase
    {
        private readonly IVwComprehensiveTimeTablesService _service;

        public VwComprehensiveController(IVwComprehensiveTimeTablesService service)
        {
            _service = service;
        }

        // GET: api/vwcomprehensive/all
        [HttpGet("all")]
        public ResponseResult<List<VwComprehensiveTimeTableVM>> GetAll()
        {
            var data = _service.GetAll();

            if (data == null || data.Count == 0)
            {
                return new ResponseResult<List<VwComprehensiveTimeTableVM>>(
                    HttpStatusCode.NotFound,
                    null,
                    "No comprehensive timetable data found."
                );
            }

            return new ResponseResult<List<VwComprehensiveTimeTableVM>>(
                HttpStatusCode.OK,
                data,
                "Comprehensive timetable data fetched successfully."
            );
        }
    }
}
