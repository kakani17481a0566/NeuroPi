using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.TableFile
{
    public class TableFileResponse
    {
        public string? Name { get; set; }

        public string? Link { get; set; }

        public static TableFileResponse ToViewModel(MTableFiles mTableFiles)
        {
            return new TableFileResponse
            {
                Name = mTableFiles.Name,
                Link = mTableFiles.Link,
            };
        }


}
    public class MTableFileResponseVM
    {
        public List<TableFileResponse> pdfs { get; set; }

        public List<TableFileResponse> videos { get; set; }





    }
}
