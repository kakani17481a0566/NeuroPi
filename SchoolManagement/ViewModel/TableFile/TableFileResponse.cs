using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.TableFile
{
    public class TableFileResponse
    {
        public string? Name { get; set; }

        public string? Link { get; set; }
        public string? Type { get; set; }



        //public int? TimeTableId { get; set; }


        public static TableFileResponse ToViewModel(MTableFiles mTableFiles)
        {
            return new TableFileResponse
            {
                Name = mTableFiles.Name,
                Link = mTableFiles.Link,
                Type = mTableFiles.Type
                //TimeTableId = mTableFiles.TimeTableId
            };
        }
    }
}
