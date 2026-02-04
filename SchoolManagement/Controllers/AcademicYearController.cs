using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.AcademicYear;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicYearController : ControllerBase
    {
        private readonly IAcademicYear _academicYearService;

        public AcademicYearController(IAcademicYear academicYearService)
        {
            _academicYearService = academicYearService;
        }

        [HttpGet]
        public ResponseResult<List<AcademicYearResponseVM>> GetAll()
        {
            var result = _academicYearService.GetAllAcadamicYears();
            return new ResponseResult<List<AcademicYearResponseVM>>(HttpStatusCode.OK, result, "All academic years fetched successfully");
        }

        [HttpGet("{id:int}")]
        public ResponseResult<AcademicYearResponseVM> GetById(int id)
        {
            var result = _academicYearService.GetAcademicYearById(id);
            if (result == null)
                return new ResponseResult<AcademicYearResponseVM>(HttpStatusCode.NotFound, null, "Academic year not found");

            return new ResponseResult<AcademicYearResponseVM>(HttpStatusCode.OK, result, "Academic year fetched successfully");
        }

        [HttpPost]
        public ResponseResult<AcademicYearResponseVM> Create([FromBody] AcademicYearCreateVm model)
        {
            var result = _academicYearService.CreateAcademicYear(model);
            return new ResponseResult<AcademicYearResponseVM>(HttpStatusCode.Created, result, "Academic year created successfully");
        }

        [HttpPut("{id:int}")]
        public ResponseResult<AcademicYearResponseVM> Update(int id, [FromBody] AcademicYearUpdateVm model)
        {
            var result = _academicYearService.UpdateAcademicYear(id, model);
            if (result == null)
                return new ResponseResult<AcademicYearResponseVM>(HttpStatusCode.NotFound, null, "Academic year not found");

            return new ResponseResult<AcademicYearResponseVM>(HttpStatusCode.OK, result, "Academic year updated successfully");
        }

        [HttpDelete("{id:int}")]
        public ResponseResult<string> Delete(int id)
        {
            var success = _academicYearService.DeleteAcademicYear(id);
            if (!success)
                return new ResponseResult<string>(HttpStatusCode.NotFound, null, "Academic year not found");

            return new ResponseResult<string>(HttpStatusCode.OK, "Deleted", "Academic year deleted successfully");
        }
    }
}
