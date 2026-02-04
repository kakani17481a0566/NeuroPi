using NeuroPi.UserManagment.ViewModel.MainMenu;

namespace NeuroPi.UserManagment.Services.Interface
{
    public interface IMainMenuService
    {
       List<MainMenuResponseVM> GetAllMainMenus(int roleId);
        List<MenuOptionsVM> GetMenuOptions();
    }
}
        