using Microsoft.AspNetCore.Mvc;
using NeuropiForms.Models;
using NeuropiForms.Services.Interface;
using NeuroPi.CommonLib.Model;
using System.Collections.Generic;

namespace NeuropiForms.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormController : ControllerBase
    {
        private readonly IFormService _formService;

        public FormController(IFormService formService)
        {
            _formService = formService;
        }

        [HttpGet]
        public ResponseResult<IEnumerable<MForm>> GetAllForms()
        {
            return _formService.GetAllForms();
        }

        [HttpGet("{id}")]
        public ResponseResult<MForm?> GetFormById(int id)
        {
            return _formService.GetFormById(id);
        }

        [HttpPost]
        public ResponseResult<MForm> CreateForm([FromBody] MForm form)
        {
            return _formService.CreateForm(form);
        }

        [HttpPut("{id}")]
        public ResponseResult<MForm?> UpdateForm(int id, [FromBody] MForm form)
        {
            return _formService.UpdateForm(id, form);
        }

        [HttpDelete("{id}")]
        public ResponseResult<bool> DeleteForm(int id)
        {
            return _formService.DeleteForm(id);
        }
    }
}
