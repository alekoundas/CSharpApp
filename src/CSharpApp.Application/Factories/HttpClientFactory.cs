namespace CSharpApp.Application.Factories
{
    public class HttpClientFactory : IHttpClientFactory
    {
        private readonly string? _baseUrl;

        public HttpClientFactory(IConfiguration configuration)
        {
            _baseUrl = configuration["BaseUrl"];
        }

        public HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl!);
            return client;
        }
    }
}
