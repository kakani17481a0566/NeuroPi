using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.PostalDeliveryItems;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostalDeliveryItemsController : ControllerBase
    {
        private readonly IPostalDeliveryItemsService _postalDeliveryItemsService;
        public PostalDeliveryItemsController(IPostalDeliveryItemsService postalDeliveryItemsService)
        {
            _postalDeliveryItemsService = postalDeliveryItemsService;
        }

        [HttpGet]

        public ResponseResult<List<PostalDeliveryItemsResponseVM>> GetAll()
        {
            var response = _postalDeliveryItemsService.GetAll();
            if (response == null || response.Count == 0)
            {
                return new ResponseResult<List<PostalDeliveryItemsResponseVM>>(HttpStatusCode.NotFound, response, "No data Found");
            }
            return new ResponseResult<List<PostalDeliveryItemsResponseVM>>(HttpStatusCode.OK, response, "Postal delivery items fetched successfully");
        }

        [HttpPost]
        public ResponseResult<string> CreatePostalDeliveryItem([FromBody] PostalDeliveryItemsRequestVM pdiRequestVM)
        {
            var response = _postalDeliveryItemsService.CreatepostalDeliveryItems(pdiRequestVM);
            if (response != null)
            {
                return new ResponseResult<string>(HttpStatusCode.OK, response, "Postal delivery item created successfully");
            }
            return new ResponseResult<string>(HttpStatusCode.BadGateway, response, "Failed to create postal delivery item");
        }

    }
}
