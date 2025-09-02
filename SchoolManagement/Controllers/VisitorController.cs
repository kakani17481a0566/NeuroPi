using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.visitors;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        private readonly VisitorInterface _visitorService;
        public VisitorController(VisitorInterface visitorService)
        {
            _visitorService = visitorService;
        }
        [HttpPost("AddVisitor")]
        public VisitorResponseVM AddVisitor(VisitorRequestVM requestVM)
        {
            return _visitorService.AddVisitor(requestVM);
        }

        [HttpPut("UpdateById/{id}")]
        public VisitorResponseVM UpdateById(int id, VisitorRequestVM requestVM)
        {
            return _visitorService.UpdateById(id, requestVM);
        }
    }
}
