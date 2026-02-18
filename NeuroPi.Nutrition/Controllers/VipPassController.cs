using Microsoft.AspNetCore.Mvc;
using NeuroPi.Nutrition.Model;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.VipPass;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace NeuroPi.Nutrition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VipPassController : ControllerBase
    {
        private readonly IVipPassService vipPassService;

        public VipPassController(IVipPassService _vipPassService)
        {
            vipPassService = _vipPassService;
        }

        [HttpPost("generate")]
        public async Task<ActionResult<List<MVipCarpidum>>> GenerateBulkPasses([FromBody] VipBulkPassRequestVM request)
        {
            try
            {
                var passes = vipPassService.GenerateBulkPasses(request);
                
                if (request.SendEmail)
                {
                    // Trigger email sending asynchronously
                    bool sent = await vipPassService.SendPassesViaEmail(request.VipEmail);
                    if (sent)
                    {
                        passes.ForEach(p => p.EmailSent = true);
                    }
                }

                return Ok(passes);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("send-email")]
        public async Task<ActionResult<bool>> SendPassesViaEmail([FromBody] string vipEmail)
        {
            try
            {
                var result = await vipPassService.SendPassesViaEmail(vipEmail);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("list")]
        public ActionResult<List<MVipCarpidum>> GetVipPasses()
        {
            return vipPassService.GetVipPasses();
        }

        [HttpGet("email/{email}")]
        public ActionResult<List<MVipCarpidum>> GetPassesByEmail(string email)
        {
            return vipPassService.GetPassesByEmail(email);
        }

        [HttpPut("validate")]
        public ActionResult<VipValidationResponseVM> ValidateVipPass([FromHeader] Guid code)
        {
            return vipPassService.ValidateVipPass(code);
        }

        [HttpDelete("id/{id}")]
        public ActionResult<bool> DeleteVipPass(int id)
        {
            var result = vipPassService.DeleteVipPass(id);
            if (result)
            {
                return Ok(true);
            }
            return NotFound(false);
        }
    }
}
