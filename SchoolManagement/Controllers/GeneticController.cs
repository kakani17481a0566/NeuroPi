using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.GeneticRegistration;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneticController : ControllerBase
    {
        private readonly IGeneticRegistrationService _geneticRegistrationService;
        public GeneticController(IGeneticRegistrationService geneticRegistrationService)
        {
            _geneticRegistrationService = geneticRegistrationService;
        }
        [HttpPost]
        public ResponseResult<string> AddGeneticRegistration([FromBody] GeneticRegistrationRequestVM requestVM)
        {
            var result=_geneticRegistrationService.AddGeneticRegistration(requestVM);
            if (result.Equals("inserted")) {
                return new ResponseResult<string>(System.Net.HttpStatusCode.OK, result, "Registered Successfully");
            }
            return new ResponseResult<string>(System.Net.HttpStatusCode.Forbidden, result, "Not Registered Please try after some time");

        }
    }
}
