using SchoolManagement.Model;
using System.Diagnostics.CodeAnalysis;

namespace SchoolManagement.ViewModel.TestContent
{
    public class TestContentVM
    {
        public string name { get; set; }
       
        public int relationId { get; set; }
        
        public IFormFile? file { get; set; }

       
        public int testId { get; set; }

        public int tenantId {  get; set; }



        public static MTestContent ToModel( TestContentVM vm)
        {
            MTestContent model = new MTestContent();
            model.name = vm.name;
            model.relationId = vm.relationId;
            //model.url = vm.url;
            model.testId = vm.testId;
            model.tenantId = vm.tenantId;
            return model;
        }
        
    }
}
