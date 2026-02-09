namespace NeuroPi.UserManagment.ViewModel.MainMenu
{
    public class MenuOptionsVM
    {
        public string mainMenus { get; set; }
        public List<Menu> menuOptions { get; set; }
    }
   public  class Menu
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //public string MainMenu { get; set; }
    }
}
