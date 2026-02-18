using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public VipPassController(IVipPassService _vipPassService, IServiceScopeFactory serviceScopeFactory)
        {
            vipPassService = _vipPassService;
            _serviceScopeFactory = serviceScopeFactory;
        }

        [HttpPost("generate")]
        public ActionResult<List<MVipCarpidum>> GenerateBulkPasses([FromBody] VipBulkPassRequestVM request)
        {
            try
            {
                var passes = vipPassService.GenerateBulkPasses(request);
                
                if (request.SendEmail)
                {
                    // Trigger email sending asynchronously in background
                    _ = Task.Run(async () =>
                    {
                        try
                        {
                            using (var scope = _serviceScopeFactory.CreateScope())
                            {
                                var scopedService = scope.ServiceProvider.GetRequiredService<IVipPassService>();
                                await scopedService.SendPassesViaEmail(request.VipEmail);
                            }
                        }
                        catch (Exception ex)
                        {
                            // Log the exception (consider using ILogger)
                            Console.WriteLine($"Background Email Error: {ex}");
                        }
                    });
                }

                return Ok(passes);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("send-email")]
        public ActionResult<bool> SendPassesViaEmail([FromBody] string vipEmail)
        {
            try
            {
                // Trigger email sending asynchronously in background
                _ = Task.Run(async () =>
                {
                    try
                    {
                        using (var scope = _serviceScopeFactory.CreateScope())
                        {
                            var scopedService = scope.ServiceProvider.GetRequiredService<IVipPassService>();
                            await scopedService.SendPassesViaEmail(vipEmail);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the exception
                        Console.WriteLine($"Background Email Error: {ex}");
                    }
                });

                return Ok(true);
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
