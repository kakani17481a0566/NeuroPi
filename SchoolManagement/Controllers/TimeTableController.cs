using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.ViewModel.Tenent;
using SchoolManagement.Model;
using SchoolManagement.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.TableFile;
using SchoolManagement.ViewModel.TimeTable;
using SchoolManagement.ViewModel.VTimeTable;
using System.Collections.Generic;
using System.Net;
using static SchoolManagement.ViewModel.TableFile.TableFileResponse;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeTableController : ControllerBase
    {
        private readonly ITimeTableServices _service;

        public TimeTableController(ITimeTableServices service)
        {
            _service = service;
        }

        [Authorize]

        [HttpGet("all")]
        public ResponseResult<List<TimeTableResponseVM>> GetAll()
        {
            var data = _service.GetAll();
            return new ResponseResult<List<TimeTableResponseVM>>(HttpStatusCode.OK, data);
        }
        
        
        [Authorize]

        [HttpGet("all/{tenantId}")]
        public ResponseResult<List<TimeTableResponseVM>> GetByTenant(int tenantId)
        {
            var data = _service.GetAll(tenantId);
            return new ResponseResult<List<TimeTableResponseVM>>(HttpStatusCode.OK, data);
        }

        [HttpGet("{id}")]
        public ResponseResult<TimeTableResponseVM> GetById(int id)
        {
            var data = _service.GetById(id);
            if (data == null)
                return new ResponseResult<TimeTableResponseVM>(HttpStatusCode.NotFound, null, "Not Found");

            return new ResponseResult<TimeTableResponseVM>(HttpStatusCode.OK, data);
        }

        [HttpGet("{id}/{tenantId}")]
        public ResponseResult<TimeTableResponseVM> GetByIdAndTenant(int id, int tenantId)
        {
            var data = _service.GetById(id, tenantId);
            if (data == null)
                return new ResponseResult<TimeTableResponseVM>(HttpStatusCode.NotFound, null, "Not Found");

            return new ResponseResult<TimeTableResponseVM>(HttpStatusCode.OK, data);
        }

        [HttpPost]
        public ResponseResult<TimeTableResponseVM> Create([FromBody] TimeTableRequestVM vm)
        {
            var data = _service.Create(vm);

            if (data == null)
            {
                return new ResponseResult<TimeTableResponseVM>(
                    HttpStatusCode.BadRequest,
                    null,
                    "Failed to create time table. Please check the input data."
                );
            }

            return new ResponseResult<TimeTableResponseVM>(
                HttpStatusCode.Created,
                data,
                "Time table created successfully"
            );
        }


        [HttpPut("{id}/{tenantId}")]
        public ResponseResult<TimeTableResponseVM> Update(int id, int tenantId, TimeTableUpdateVM vm)
        {
            var data = _service.Update(id, tenantId, vm);
            if (data == null)
                return new ResponseResult<TimeTableResponseVM>(HttpStatusCode.NotFound, null, "Not Found");

            return new ResponseResult<TimeTableResponseVM>(HttpStatusCode.OK, data);
        }

        [HttpDelete("{id}/{tenantId}")]
        public ResponseResult<string> Delete(int id, int tenantId)
        {
            var success = _service.Delete(id, tenantId);
            if (!success)
                return new ResponseResult<string>(HttpStatusCode.NotFound, null, "Not Found");

            return new ResponseResult<string>(HttpStatusCode.OK, "Deleted successfully");
        }

        [Authorize]
        [HttpGet("weekId/{weekId}/tenantId/{tenantId}/courseId/{courseId}")]
        public ResponseResult<WeekTimeTableData> GetWeeklyTimeTable(int weekId, int tenantId, int courseId)
        {
            var data = _service.GetWeeklyTimeTable(weekId, tenantId, courseId);

            if (data == null)
                return new ResponseResult<WeekTimeTableData>(HttpStatusCode.NotFound, null, "Not Found");

            return new ResponseResult<WeekTimeTableData>(HttpStatusCode.OK, data);
        }

        //[HttpGet("files/{courseId}")]

        //public ResponseResult<MTableFileResponseVM> GetAllFilesByCourseId(int courseId)
        //{
        //    var data = _service.GetAllByCourseId(courseId);
        //    if (data != null)
        //    {
        //        return new ResponseResult<MTableFileResponseVM>(HttpStatusCode.OK, data,"Files fetched successfully");
        //    }
        //    return new ResponseResult<MTableFileResponseVM>(HttpStatusCode.NotFound, data, "Not Found");


        //}



        [HttpGet("/week/{courseId}/{tenantId}")]
        public ResponseResult<String> GetWeekTimeTable([FromQuery]int courseId,int tenantId)
        {
            var result=_service.GetWeekTimeTable(courseId, tenantId);
            return new ResponseResult<string>(HttpStatusCode.OK, result, "Data Fetched successfully");
        }

        [Authorize]

        [HttpGet("structured/{tenantId}")]
        public ResponseResult<TimeTableData> GetStructuredTimeTable(int tenantId)
        {
            var data = _service.GetAllStructured(tenantId);
            return new ResponseResult<TimeTableData>(HttpStatusCode.OK, data, "Structured Time Table Data");
        }

        [HttpGet("insert-options-time-table/{tenantId}")]
        public ResponseResult<TimeTableInsertTableOptionsVM> GetInsertOptions(int tenantId)
        {
            var result = _service.GetInsertOptions(tenantId);
            if (result != null)
            {
                return new ResponseResult<TimeTableInsertTableOptionsVM>(
                    HttpStatusCode.OK, result, "Insert options fetched"
                );
            }
            return new ResponseResult<TimeTableInsertTableOptionsVM>(HttpStatusCode.NotFound, result, "No insert options found");
        }


    }
}
