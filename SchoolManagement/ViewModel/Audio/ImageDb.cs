namespace SchoolManagement.ViewModel.Audio
{
    public class ImageDb
    {
        public string name {  get; set; }

        public byte[]  url {  get; set; }
        public ImageDb(string name, byte[] url)
        {
            this.name = name;
            this.url = url;
        }
        public ImageDb()
        {
            
        }
    }
}
