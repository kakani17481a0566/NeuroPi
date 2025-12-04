using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Subject
{
    public class SubjectResponseVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        

    

        public static SubjectResponseVM FromModel(MSubject model) => new SubjectResponseVM
        {
            Id = model.Id,
            Name = model.Name,
            Code = model.Code,
            Description = model.Description
        };
    }
}
