﻿using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.TimeTableDetail;
using System.Collections.Generic;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeTableDetailController : ControllerBase
    {
        private readonly ITimeTableDetailService _service;

        public TimeTableDetailController(ITimeTableDetailService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        public ResponseResult<List<TimeTableDetailResponseVM>> GetAll()
        {
            var data = _service.GetAll();
            return new ResponseResult<List<TimeTableDetailResponseVM>>(HttpStatusCode.OK, data);
        }

        [HttpGet("all/{tenantId}")]
        public ResponseResult<List<TimeTableDetailResponseVM>> GetAllByTenant(int tenantId)
        {
            var data = _service.GetAll(tenantId);
            return new ResponseResult<List<TimeTableDetailResponseVM>>(HttpStatusCode.OK, data);
        }

        [HttpGet("{id}")]
        public ResponseResult<TimeTableDetailResponseVM> GetById(int id)
        {
            var data = _service.GetById(id);
            if (data == null)
                return new ResponseResult<TimeTableDetailResponseVM>(HttpStatusCode.NotFound, null, "Not Found");

            return new ResponseResult<TimeTableDetailResponseVM>(HttpStatusCode.OK, data);
        }

        [HttpGet("{id}/{tenantId}")]
        public ResponseResult<TimeTableDetailResponseVM> GetByIdTenant(int id, int tenantId)
        {
            var data = _service.GetById(id, tenantId);
            if (data == null)
                return new ResponseResult<TimeTableDetailResponseVM>(HttpStatusCode.NotFound, null, "Not Found");

            return new ResponseResult<TimeTableDetailResponseVM>(HttpStatusCode.OK, data);
        }

        [HttpPost]
        public ResponseResult<TimeTableDetailResponseVM> Create(TimeTableDetailRequestVM vm)
        {
            var data = _service.Create(vm);
            return new ResponseResult<TimeTableDetailResponseVM>(HttpStatusCode.Created, data);
        }

        [HttpPut("{id}/{tenantId}")]
        public ResponseResult<TimeTableDetailResponseVM> Update(int id, int tenantId, TimeTableDetailUpdateVM vm)
        {
            var data = _service.Update(id, tenantId, vm);
            if (data == null)
                return new ResponseResult<TimeTableDetailResponseVM>(HttpStatusCode.NotFound, null, "Not Found");

            return new ResponseResult<TimeTableDetailResponseVM>(HttpStatusCode.OK, data);
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
