using NeuroPi.UserManagment.Model;
using NeuroPi.UserManagment.ViewModel.MainMenu;


namespace NeuroPi.UserManagment.Services.Interface
{
    public interface IMainMenuService
    {
        List<MainMenuResponseVM> GetAllMainMenus(int roleId);
        List<MenuOptionsVM> GetMenuOptions();
        List<MenuOptionsVM> GetOptions(int roleId);
        List<MMenu> GetMenusByTenant(int tenantId);
        MMenu GetMenuById(int id, int tenantId);
        MMenu CreateMenu(MMenu menu);
        MMenu UpdateMenu(int id, int tenantId, MMenu menu);
        bool DeleteMenu(int id, int tenantId);
    }
}
        