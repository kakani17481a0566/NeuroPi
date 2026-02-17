using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using NeuropiForms.Services.Interface;
using NeuropiForms.ViewModels.Fields;
using System.Net;

namespace NeuropiForms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldController : ControllerBase
    {
        private readonly IFieldService _fieldService;

        public FieldController(IFieldService fieldService)
        {
            _fieldService = fieldService;
        }

        [HttpGet]
        public ResponseResult<List<FieldsResponseVM>> GetAllFields()
        {
            var fields = _fieldService.GetAllFields();
            return new ResponseResult<List<FieldsResponseVM>>(HttpStatusCode.OK, fields);
        }

        [HttpGet("id{id}")]
        public ResponseResult<FieldsResponseVM> GetFieldById(int id)
        {
            var field = _fieldService.GetFieldById(id);
            if (field == null)
                return new ResponseResult<FieldsResponseVM>(HttpStatusCode.NotFound, null, "Field not found");
            return new ResponseResult<FieldsResponseVM>(HttpStatusCode.OK, field);
        }

        [HttpGet("tenantId/{tenantId}id/{id}")]
        public ResponseResult<FieldsResponseVM> GetFieldByIdAndTenantId(int tenantId, int id)
        {
            var field = _fieldService.GetFieldByIdAndTenantId(id, tenantId);
            if (field == null)
                return new ResponseResult<FieldsResponseVM>(HttpStatusCode.NotFound, null, "Field not found for the given tenant");
            return new ResponseResult<FieldsResponseVM>(HttpStatusCode.OK, field);
        }

        [HttpGet("tenantId/{tenantId}")]
        public ResponseResult<FieldsResponseVM> GetFieldByTenantId(int tenantId)
        {
            var field = _fieldService.GetFieldByTenantId(tenantId);
            if (field == null)
                return new ResponseResult<FieldsResponseVM>(HttpStatusCode.NotFound, null, "Field not found for the given tenant");
            return new ResponseResult<FieldsResponseVM>(HttpStatusCode.OK, field);
        }

        [HttpPost]
        public ResponseResult<FieldsResponseVM> CreateField(FieldsRequestVM field)
        {
            var createdField = _fieldService.CreateField(field);
            return new ResponseResult<FieldsResponseVM>(HttpStatusCode.Created, createdField);

        }
        [HttpPut("id/tenantId/{id}/{tenantId}")]
        public ResponseResult<FieldsResponseVM> UpdateField(int id, int tenantId, FieldsUpdateVM field)
        {
            var updatedField = _fieldService.UpdateField(id, tenantId, field);
            if (updatedField == null)
                return new ResponseResult<FieldsResponseVM>(HttpStatusCode.NotFound, null, "Field not found for the given tenant");
            return new ResponseResult<FieldsResponseVM>(HttpStatusCode.OK, updatedField);
        }
         [HttpDelete("id/tenantId/{id}/{tenantId}")]
         public ResponseResult<bool> DeleteField(int id, int tenantId)
            {
                var isDeleted = _fieldService.DeleteField(id, tenantId);
                if (!isDeleted)
                    return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Field not found for the given tenant");
                return new ResponseResult<bool>(HttpStatusCode.OK, true);
        }
    }


}
