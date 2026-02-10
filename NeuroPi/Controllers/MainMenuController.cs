using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Model;
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
        [HttpGet("menuoptions")]
        public ResponseResult<List<MenuOptionsVM>> GetAllOptions()
        {
            var result = _mainMenuService.GetMenuOptions();
            return new ResponseResult<List<MenuOptionsVM>>(HttpStatusCode.OK, result, "options fetched successfully");
        }
        [HttpGet("menuOptions/new/{roleId}")]
        public ResponseResult<List<MenuOptionsVM>> GetOptions( int roleId)
        {
            var result = _mainMenuService.GetOptions(roleId);
            return new ResponseResult<List<MenuOptionsVM>>(HttpStatusCode.OK, result, "options fetched successfully");
        }

        // CRUD Endpoints
        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<MMenu>> GetMenusByTenant(int tenantId)
        {
            var result = _mainMenuService.GetMenusByTenant(tenantId);
            return new ResponseResult<List<MMenu>>(HttpStatusCode.OK, result, "Menus fetched successfully");
        }

        [HttpGet("{id}/{tenantId}")]
        public ResponseResult<MMenu> GetMenuById(int id, int tenantId)
        {
            var result = _mainMenuService.GetMenuById(id, tenantId);
            if (result == null)
                return new ResponseResult<MMenu>(HttpStatusCode.NotFound, null, "Menu not found");
            return new ResponseResult<MMenu>(HttpStatusCode.OK, result, "Menu fetched successfully");
        }

        [HttpPost]
        public ResponseResult<MMenu> CreateMenu([FromBody] MMenu menu)
        {
            var result = _mainMenuService.CreateMenu(menu);
            return new ResponseResult<MMenu>(HttpStatusCode.Created, result, "Menu created successfully");
        }

        [HttpPut("{id}/{tenantId}")]
        public ResponseResult<MMenu> UpdateMenu(int id, int tenantId, [FromBody] MMenu menu)
        {
            var result = _mainMenuService.UpdateMenu(id, tenantId, menu);
            if (result == null)
                return new ResponseResult<MMenu>(HttpStatusCode.NotFound, null, "Menu not found");
            return new ResponseResult<MMenu>(HttpStatusCode.OK, result, "Menu updated successfully");
        }

        [HttpDelete("{id}/{tenantId}")]
        public ResponseResult<object> DeleteMenu(int id, int tenantId)
        {
            var result = _mainMenuService.DeleteMenu(id, tenantId);
            if (!result)
                return new ResponseResult<object>(HttpStatusCode.NotFound, null, "Menu not found");
            return new ResponseResult<object>(HttpStatusCode.OK, null, "Menu deleted successfully");
        }

        [HttpPost("generateqrcode")]
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
