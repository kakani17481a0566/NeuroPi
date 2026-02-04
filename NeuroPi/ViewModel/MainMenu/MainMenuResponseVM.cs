using NeuroPi.UserManagment.Model;

namespace NeuroPi.UserManagment.ViewModel.MainMenu
{
    public class MainMenuResponseVM
    {
        public string id { get; set; }

        public string type { get; set; }

        public string path { get; set; }
        public string title { get; set; }

        public string transkey {  get; set; }
        public string Icon { get; set; }

        public List<MenuResponseVM> childs { get; set; }


        
    }
}
