using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.TableFile;
using SchoolManagement.ViewModel.TableFiles;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableFilesController : ControllerBase
    {
        private readonly ITableFilesService _service;

        public TableFilesController(ITableFilesService service)
        {
            _service = service;
        }

        [HttpPost]
        public ResponseResult<TimetableAttachmentVM> Create(TimetableAttachmentCreateVM vm)
        {
            var data = _service.Create(vm);
            return new ResponseResult<TimetableAttachmentVM>(HttpStatusCode.Created, data, "Created successfully");
        }

        [HttpPut("{id}/tenant/{tenantId}")]
        public ResponseResult<TimetableAttachmentVM> Update(int id, int tenantId, TimetableAttachmentUpdateVM vm)
        {
            var data = _service.Update(id, vm, tenantId);
            if (data == null)
                return new ResponseResult<TimetableAttachmentVM>(HttpStatusCode.NotFound, null, "File not found");

            return new ResponseResult<TimetableAttachmentVM>(HttpStatusCode.OK, data, "Updated successfully");
        }

        [HttpDelete("{id}/tenant/{tenantId}")]
        public ResponseResult<string> Delete(int id, int tenantId)
        {
            var ok = _service.Delete(id, tenantId);
            if (!ok)
                return new ResponseResult<string>(HttpStatusCode.NotFound, null, "File not found");

            return new ResponseResult<string>(HttpStatusCode.OK, null, "Deleted successfully");
        }

        [HttpGet("{id}/tenant/{tenantId}")]
        public ResponseResult<TimetableAttachmentVM> GetById(int id, int tenantId)
        {
            var data = _service.GetById(id, tenantId);
            if (data == null)
                return new ResponseResult<TimetableAttachmentVM>(HttpStatusCode.NotFound, null, "File not found");

            return new ResponseResult<TimetableAttachmentVM>(HttpStatusCode.OK, data);
        }

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<TimetableAttachmentVM>> GetAll(int tenantId)
        {
            var data = _service.GetAll(tenantId);
            return new ResponseResult<List<TimetableAttachmentVM>>(HttpStatusCode.OK, data);
        }

        [HttpGet("course/{courseId}/timetable/{timeTableId}/tenant/{tenantId}")]
        public ResponseResult<List<TimetableAttachmentVM>> GetByCourseAndTimetable(
            int courseId, int timeTableId, int tenantId)
        {
            int? ttId = timeTableId == 0 ? null : timeTableId;
            var data = _service.GetByCourseAndTimeTable(courseId, ttId, tenantId);

            return new ResponseResult<List<TimetableAttachmentVM>>(HttpStatusCode.OK, data);
        }
    }
}
