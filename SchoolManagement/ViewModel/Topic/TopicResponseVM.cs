using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Topic
{
    public class TopicResponseVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int SubjectId { get; set; }
        public int TopicTypeId { get; set; }
        public int TenantId { get; set; }

        public static TopicResponseVM FromModel(MTopic model)
        {
            if (model == null) return null;

            return new TopicResponseVM
            {
                Id = model.Id,
                Name = model.Name,
                Code = model.Code,
                Description = model.Description,
                SubjectId = model.SubjectId,
                TopicTypeId = model.TopicTypeId,
                TenantId = model.TenantId
            };
        }
    }
}
