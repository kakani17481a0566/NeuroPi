namespace SchoolManagement.ViewModel.Audio
{
    public class ImageDb
    {
        public string name {  get; set; }
        public int TestId {  get; set; }

        public int RelationId { get; set; }

        public int TestContentId { get; set; }

        public byte[]  url {  get; set; }
        public ImageDb(string name, byte[] url,int testId,int relationId,int testContentId)
        {
            this.name = name;
            this.url = url;
            this.RelationId = relationId;
            this.TestId = testId;
            this.TestContentId = testContentId;
        }
        public ImageDb()
        {
            
        }
    }
}
