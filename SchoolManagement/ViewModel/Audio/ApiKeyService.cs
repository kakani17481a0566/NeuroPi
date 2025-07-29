
using Microsoft.Extensions.Options;

namespace SchoolManagement.ViewModel.Audio
{
    public class ApiKeyService
    {

        public string GetAzureApiKey()
        {
            return Environment.GetEnvironmentVariable("AZURE_API_KEY");
        }

        public string GetGoogleApiKey()
        {
            return Environment.GetEnvironmentVariable("GOOGLE_API_KEY");
        }

     
        

}

}
