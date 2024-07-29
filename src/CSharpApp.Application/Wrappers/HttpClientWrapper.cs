using Newtonsoft.Json;

namespace CSharpApp.Application.Wrappers
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly ILogger<HttpClientWrapper> _logger;
        private readonly HttpClient _client;

        public HttpClientWrapper(ILogger<HttpClientWrapper> logger,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _client = httpClientFactory.CreateHttpClient();
        }

        public async Task<ReadOnlyCollection<TEntity>?> GetAllRecords<TEntity>(string url)
        {
            var response = await _client.GetFromJsonAsync<List<TEntity>>(url);

            if(response == null)
                return null;

            return response.AsReadOnly();
        }

        public async Task<TEntity?> GetRecordById<TEntity>(string url)
        {
            var response = await _client.GetFromJsonAsync<TEntity>(url);
            return response;
        }

        public async Task<TEntity?> InsertRecord<TEntity>(string url, TEntity record)
        {
            var response = await _client.PostAsJsonAsync(url, record);
            var responseBody = await response.Content.ReadAsStringAsync();
            var responseRecord = JsonConvert.DeserializeObject<TEntity>(responseBody);

            return responseRecord;
        }

        public async Task<TEntity?> UpdateRecord<TEntity>(string url, TEntity record)
        {
            var response = await _client.PutAsJsonAsync(url, record);
            var responseBody = await response.Content.ReadAsStringAsync();
            var responseRecord = JsonConvert.DeserializeObject<TEntity>(responseBody);

            return responseRecord;
        }

        public async Task<TEntity?> DeleteRecord<TEntity>(string url)
        {
            var response = await _client.DeleteFromJsonAsync<TEntity>(url);
            return response;
        }
    }
}
