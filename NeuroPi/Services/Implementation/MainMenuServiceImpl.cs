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
           
            var roleMenu = _context.RolePermissions.Where(r => r.RoleId == roleId && !r.IsDeleted).Include(i => i.Menu.MainMenu).ToList();
            if (roleMenu != null)
            {

                return roleMenu.Select(r => new MainMenuResponseVM()
                {
                    id = r.Menu.MainMenu.Name,
                    path = r.Menu.MainMenu.Path,
                    title = r.Menu.MainMenu.Title,
                    type = r.Menu.MainMenu.Type,
                    transkey = r.Menu.MainMenu.Transkey,
                    Icon = r.Menu.MainMenu.Icon,
                    childs = r.Menu.MainMenu.Menus.Select(r => new MenuResponseVM()
                    {
                        id = r.Name,
                        path = r.Path,
                        type = r.Type,
                        title = r.Title,
                        transkey = r.TransKey,
                        Icon=r.Icon

                    }).ToList(),

                }).GroupBy(x => x.id)
                  .Select(g => g.First()).ToList();

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

        public List<MenuOptionsVM> GetOptions(int roleId)
        {
           var rolemenus=_context.RolePermissions.Where(r=>!r.IsDeleted && r.RoleId==roleId).ToList();
           var menus=_context.Menu.Include(m=>m.MainMenu).Where(r=>!r.IsDeleted).ToList();
            IEnumerable<int> assignedIds = rolemenus.Select(menu => menu.MenuId);
            IEnumerable<int> completeIds = menus.Select(menu => menu.Id);
            IEnumerable<int> remainingMenuIds = completeIds.Except(assignedIds);
              var options = menus.Where(m => remainingMenuIds.Contains(m.Id)).GroupBy(m => m.MainMenu.Title)
                .Select(g => new MenuOptionsVM {
                    mainMenus = g.Key,menuOptions = g.Select(m => new Menu{Id = m.Id, Name = m.Title}).ToList()}).ToList();
            return options;

        }
    }
}
