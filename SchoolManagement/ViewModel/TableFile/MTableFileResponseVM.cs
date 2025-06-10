using System.Collections.Generic;

namespace SchoolManagement.ViewModel.TableFile
{
    
    public class MTableFileResponseVM
    {
        public List<TableFileResponse> Pdfs { get; set; } = new();

        public List<TableFileResponse> Videos { get; set; } = new();
    }
}
