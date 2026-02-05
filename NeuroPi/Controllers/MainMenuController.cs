using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.MainMenu;
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
        [HttpGet("/{roleId}")]
        public ResponseResult<List<MainMenuResponseVM>> GetAllMainMenus( int roleId)
        {
            var result=_mainMenuService.GetAllMainMenus(roleId);
            return new ResponseResult<List<MainMenuResponseVM>>(HttpStatusCode.OK, result, "all menus fetched successfully");
        }
        [HttpGet("/menuoptins")]
        public ResponseResult<List<MenuOptionsVM>> GetAllOptions()
        {
            var result = _mainMenuService.GetMenuOptions();
            return new ResponseResult<List<MenuOptionsVM>>(HttpStatusCode.OK, result, "options fetched successfully");
        }
    }
}
