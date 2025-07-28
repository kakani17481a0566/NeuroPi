
using Microsoft.Extensions.Options;

namespace SchoolManagement.ViewModel.Audio
{
    public class ApiKeyService
    {
        private readonly IConfiguration _configuration;
        public  string googleApiKey { get; set; }
        public  string azureApiKey { get; set; }

        private readonly string _azureApiKey;
        private readonly string _googleApiKey;

        // Step 4: Inject IOptions<ApiKeys> into the constructor
        public ApiKeyService(IOptions<ApiKeys> apiKeysOptions)
        {
            // Access the ApiKeys object via the .Value property
            var apiKeys = apiKeysOptions.Value;

            // Null-conditional operator for robustness, though Get<T> usually ensures non-null if section exists
            _azureApiKey = apiKeys?.AzureApiKey;
            _googleApiKey = apiKeys?.GoogleApiKey;

            // You can add logging here to confirm keys are loaded (for debugging)
            // Using .NET's built-in ILogger is preferable to Console.WriteLine in real apps
            Console.WriteLine($"MyApiClientService initialized. Azure Key Loaded: {!string.IsNullOrEmpty(_azureApiKey)}");
            Console.WriteLine($"MyApiClientService initialized. Google Key Loaded: {!string.IsNullOrEmpty(_googleApiKey)}");
        }

        public string GetAzureApiKey()
        {
            return _azureApiKey;
        }

        public string GetGoogleApiKey()
        {
            return _googleApiKey;
        }

        // Example method that would use the Azure API Key
        //public void CallAzureService()
        //{
        //    Console.WriteLine($"Calling Azure service with key: {_azureApiKey}");
        //    // Your actual API call logic here
        //}

        //// Example method that would use the Google API Key
        //public void CallGoogleService()
        //{
        //    Console.WriteLine($"Calling Google service with key: {_googleApiKey}");
        

}

}
