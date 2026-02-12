using Microsoft.AspNetCore.Mvc;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.QrCode;
using NeuroPi.CommonLib.Model;
using System.Collections.Generic;


namespace NeuroPi.Nutrition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerateQrCodeController : ControllerBase
    {
        public readonly GenerateQrCodeService qrCodeService;
        public GenerateQrCodeController(GenerateQrCodeService _qrCodeService)
        {
            qrCodeService = _qrCodeService;
        }
        //[HttpPost("generateqrcode")]
        //public string GenerateQrCode()
        //{
        //    return qrCodeService.GenerateQrCode();
        //}
        [HttpPut("validation")]
        public ActionResult<QrCodeValidationResponseVM> ValidateQrCode([FromHeader] Guid code)
        {
            return qrCodeService.ValidateQrCode(code);
        }
        [HttpPost("register")]
        public string RegisterForCarpedium([FromBody] QrCodeRequestVM carpedium)
        {
            var result = qrCodeService.AddCarpidiumDetails(carpedium);
            return result;

            
        }

        [HttpGet("guests")]
        public ActionResult<List<MCarpidum>> GetGuestList()
        {
            return qrCodeService.GetGuestList();
        }
        [HttpGet("student/{studentId}")]
        public ActionResult<List<MCarpidum>> GetPassesByStudentId(int studentId)
        {
            return qrCodeService.GetPassesByStudentId(studentId);
        }

        [HttpDelete("id/{id}")]
        public ActionResult<bool> DeletePass(int id)
        {
            var result = qrCodeService.DeletePass(id);
            if (result)
            {
                return Ok(true);
            }
            return NotFound(false);
        }
    }
}
