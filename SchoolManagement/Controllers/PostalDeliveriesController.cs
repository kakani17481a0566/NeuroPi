using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CognitiveServices.Speech.Audio;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.PostalDeliveries;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostalDeliveriesController : ControllerBase
    {
        private readonly IPostalDeliveriesService _postalDeliveriesService;
        public PostalDeliveriesController(IPostalDeliveriesService postalDeliveriesService)
        {
            _postalDeliveriesService = postalDeliveriesService;
        }

        [HttpGet]
        public ResponseResult<List<PostalDeliveriesResponseVM>> GetAll()
        {
            var response = _postalDeliveriesService.GetAll();
            if (response == null || response.Count == 0)
            {
                return new ResponseResult<List<PostalDeliveriesResponseVM>>(HttpStatusCode.NotFound, response, "No data Found");
            }
            return new ResponseResult<List<PostalDeliveriesResponseVM>>(HttpStatusCode.OK, response, "Postal deliveries fetched successfully");
        }

        [HttpPost]
        public ResponseResult<PostalDeliveriesResponseVM> CreatePostalDelivery([FromBody] PostalDeliveriesRequestVM pdRequestVM)
        {
            var response = _postalDeliveriesService.CreatePostalDelivery(pdRequestVM);
            if (response != null)
            {
                return new ResponseResult<PostalDeliveriesResponseVM>(HttpStatusCode.OK, response, "Postal delivery created successfully");
            }
            return new ResponseResult<PostalDeliveriesResponseVM>(HttpStatusCode.BadGateway, response, "Failed to create postal delivery");
        }

    }
}
