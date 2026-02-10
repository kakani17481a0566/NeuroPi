using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.MainMenu;
using QRCoder;
using System.Drawing;
using System.Net;

namespace NeuroPi.UserManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainMenuController : ControllerBase
    {
        private readonly IMainMenuService _mainMenuService;
        public MainMenuController(IMainMenuService mainMenuService)
        {
            _mainMenuService = mainMenuService;
        }
        [HttpGet("roleId/{roleId}")]
        public ResponseResult<List<MainMenuResponseVM>> GetAllMainMenus(int roleId)
        {
            var result = _mainMenuService.GetAllMainMenus(roleId);
            return new ResponseResult<List<MainMenuResponseVM>>(HttpStatusCode.OK, result, "all menus fetched successfully");
        }
        [HttpGet("/menuoptions")]
        public ResponseResult<List<MenuOptionsVM>> GetAllOptions()
        {
            var result = _mainMenuService.GetMenuOptions();
            return new ResponseResult<List<MenuOptionsVM>>(HttpStatusCode.OK, result, "options fetched successfully");
        }
        [HttpGet("/menuOptions/new/{roleId}")]
        public ResponseResult<List<MenuOptionsVM>> GetOptions( int roleId)
        {
            var result = _mainMenuService.GetOptions(roleId);
            return new ResponseResult<List<MenuOptionsVM>>(HttpStatusCode.OK, result, "options fetched successfully");
        }
        [HttpPost("/generateqrcode")]
        public string GenerateQrCode(string inputData, int pixelsPerModule = 20)
        {
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(inputData, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new QRCode(qrCodeData);
            using Bitmap qrCodeAsBitmap = qrCode.GetGraphic(pixelsPerModule);
            using var memoryStream = new MemoryStream();
            qrCodeAsBitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
            string base64String = Convert.ToBase64String(memoryStream.ToArray());
            return "data:image/png;base64," + base64String;
        }
    }
}
