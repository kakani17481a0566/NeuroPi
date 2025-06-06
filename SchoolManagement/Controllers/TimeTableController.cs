﻿using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.TimeTable;
using System.Collections.Generic;
using System.Net;

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
    }
}
