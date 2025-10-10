using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.PostTransactionMaster;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PosTransactionMasterController : ControllerBase
    {
        private readonly IPosTransactionMasterService posTransactionMasterService;

        public PosTransactionMasterController(IPosTransactionMasterService posTransactionMasterService)
        {
            this.posTransactionMasterService = posTransactionMasterService;
        }

        [HttpPost("CreatePostTransaction")]
        public string CreatePostTransaction([FromBody] PosTransactionMasterRequestVM request)
        {
            return posTransactionMasterService.CreatePostTransaction(request);
        }


    }
}
