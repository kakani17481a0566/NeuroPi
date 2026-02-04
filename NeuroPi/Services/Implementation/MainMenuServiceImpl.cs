using Microsoft.EntityFrameworkCore;
using NeuroPi.UserManagment.Data;
using NeuroPi.UserManagment.Model;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.MainMenu;

namespace NeuroPi.UserManagment.Services.Implementation
{
    public class MainMenuServiceImpl : IMainMenuService
    {
        private readonly NeuroPiDbContext _context;
        public MainMenuServiceImpl(NeuroPiDbContext context) {
            _context = context;
        }
        public List<MainMenuResponseVM> GetAllMainMenus(int roleId)
        {
            var result=_context.MainMenus.Where(m=>!m.IsDeleted).ToList();
            var menuResults=_context.Menu.Where(m=>!m.IsDeleted).ToList();
            if (result.Count > 0)
            { 
                    var menus = menuResults.Select(item => new MenuResponseVM
                    {
                        id = item.Name,
                        path = item.Path,
                        type = item.Type,
                        title = item.Title,
                        transkey = item.TransKey,
                        Icon = item.Icon
                    }).ToList();
                    var responseVMs = result.Select(menu => new MainMenuResponseVM
                    {
                        id = menu.Name,
                        path = menu.Path,
                        type = menu.Type,
                        title = menu.Title,
                        transkey = menu.Transkey,
                        Icon = menu.Icon,
                        childs = menus
                    }).ToList();
                return responseVMs;
            }
            return null;
        }

        public List<MenuOptionsVM> GetMenuOptions()
        {
            List<MenuOptionsVM> options = new List<MenuOptionsVM>();
            var result=_context.MainMenus.Include(M=>M.Menus).Where(i=>!i.IsDeleted).ToList();
            if (result.Count > 0)
            {
                foreach (var menu in result)
                {
                   MenuOptionsVM optionsVM = new MenuOptionsVM();
                    optionsVM.mainMenus = menu.Title;
                    optionsVM.menuOptions=menu.Menus.Select(i => new Menu()
                    {
                        Id = i.Id,
                        Name = i.Title,
                    }).ToList();
                    options.Add(optionsVM);
                }
                return options;

                //result.Select(m=>m.Menus).ToList();
            }
            return null;
        }
    }
}
