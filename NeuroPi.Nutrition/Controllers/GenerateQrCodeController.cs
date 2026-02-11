using Microsoft.AspNetCore.Mvc;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.QrCode;


namespace NeuroPi.Nutrition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerateQrCodeController : ControllerBase
    {
        public readonly GenerateQrCodeService qrCodeService;
        public GenerateQrCodeController(GenerateQrCodeService _qrCodeService)
        {
            qrCodeService= _qrCodeService;
        }
        //[HttpPost("/generateqrcode")]
        //public string GenerateQrCode()
        //{
        //    return qrCodeService.GenerateQrCode();
        //}
        [HttpPut("/validation")]
        public ActionResult<QrCodeValidationResponseVM> ValidateQrCode([FromHeader] Guid code)
        {
            return qrCodeService.ValidateQrCode(code);
        }
        [HttpPost("/register")]
        public string RegisterForCarpedium([FromBody] QrCodeRequestVM carpedium)
        {
            var result= qrCodeService.AddCarpidiumDetails(carpedium);
            return result;

        }
    }
}
