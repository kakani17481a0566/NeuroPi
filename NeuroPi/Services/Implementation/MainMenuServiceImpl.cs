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
            // Get all menu IDs that this role has permission for
            var roleMenuIds = _context.RolePermissions
                .Where(r => r.RoleId == roleId && !r.IsDeleted)
                .Select(r => r.MenuId)
                .ToHashSet();

            var roleMenu = _context.RolePermissions
                .Where(r => r.RoleId == roleId && !r.IsDeleted)
                .Include(i => i.Menu.MainMenu)
                .ThenInclude(mm => mm.Menus) // Include all menus under the main menu
                .ToList();

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
                    // ✅ FIX: Only include child menus that the role has permission for
                    childs = r.Menu.MainMenu.Menus
                        .Where(m => roleMenuIds.Contains(m.Id) && !m.IsDeleted)
                        .Select(m => new MenuResponseVM()
                        {
                            id = m.Name,
                            path = m.Path,
                            type = m.Type,
                            title = m.Title,
                            transkey = m.TransKey,
                            Icon = m.Icon
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

        // CRUD operations
        public List<MMenu> GetMenusByTenant(int tenantId)
        {
            return _context.Menu
                .Where(m => m.TenantId == tenantId && !m.IsDeleted)
                .OrderBy(m => m.Name)
                .ToList();
        }

        public MMenu GetMenuById(int id, int tenantId)
        {
            return _context.Menu
                .FirstOrDefault(m => m.Id == id && m.TenantId == tenantId && !m.IsDeleted);
        }

        public MMenu CreateMenu(MMenu menu)
        {
            menu.IsDeleted = false;
            _context.Menu.Add(menu);
            _context.SaveChanges();
            return menu;
        }

        public MMenu UpdateMenu(int id, int tenantId, MMenu menu)
        {
            var existing = _context.Menu
                .FirstOrDefault(m => m.Id == id && m.TenantId == tenantId && !m.IsDeleted);
            
            if (existing == null) return null;

            existing.Name = menu.Name;
            existing.Title = menu.Title;
            existing.Path = menu.Path;
            existing.Icon = menu.Icon;
            existing.TransKey = menu.TransKey;
            existing.Type = menu.Type;

            _context.SaveChanges();
            return existing;
        }

        public bool DeleteMenu(int id, int tenantId)
        {
            var menu = _context.Menu
                .FirstOrDefault(m => m.Id == id && m.TenantId == tenantId && !m.IsDeleted);
            
            if (menu == null) return false;

            menu.IsDeleted = true;
            _context.SaveChanges();
            return true;
        }
    }
}
