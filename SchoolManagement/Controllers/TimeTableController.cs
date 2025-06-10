using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("all")]
        public ResponseResult<List<TimeTableResponseVM>> GetAll()
        {
            var data = _service.GetAll();
            return new ResponseResult<List<TimeTableResponseVM>>(HttpStatusCode.OK, data);
        }

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
        public ResponseResult<TimeTableResponseVM> Create(TimeTableRequestVM vm)
        {
            var data = _service.Create(vm);
            return new ResponseResult<TimeTableResponseVM>(HttpStatusCode.Created, data);
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

        [HttpGet("weekId/{weekId}/tenantId/{tenantId}/courseId/{courseId}")]
        public ResponseResult<WeekTimeTableData> GetWeeklyTimeTable(int weekId, int tenantId, int courseId)
        {
            var data = _service.GetWeeklyTimeTable(weekId, tenantId, courseId);

            if (data == null)
                return new ResponseResult<WeekTimeTableData>(HttpStatusCode.NotFound, null, "Not Found");

            return new ResponseResult<WeekTimeTableData>(HttpStatusCode.OK, data);
        }

        [HttpGet("files/{courseId}")]

        public ResponseResult<MTableFileResponseVM> GetAllFilesByCourseId(int courseId)
        {
            var data = _service.GetAllByCourseId(courseId);
            if (data != null)
            {
                return new ResponseResult<MTableFileResponseVM>(HttpStatusCode.OK, data,"Files fetched successfully");
            }
            return new ResponseResult<MTableFileResponseVM>(HttpStatusCode.NotFound, data, "Not Found");


        }



    }
}
