using Newtonsoft.Json;

namespace CSharpApp.Application.Services;

public class PostService : IPostService
{
    private readonly ILogger<PostService> _logger;
    private readonly HttpClient _client;

    public PostService(ILogger<PostService> logger,
        IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _client = httpClientFactory.CreateHttpClient();
    }

    public async Task<PostRecord?> GetByRecordId(int id)
    {
        var response = await _client.GetFromJsonAsync<PostRecord>($"posts/{id}");
        return response;
    }

    public async Task<ReadOnlyCollection<PostRecord>> GetAllRecords()
    {
        var response = await _client.GetFromJsonAsync<List<PostRecord>>($"posts");
        return response!.AsReadOnly();
    }

    public async Task<PostRecord?> InsertRecord(PostRecord postRecord)
    {
        var response = await _client.PostAsJsonAsync($"posts", postRecord);
        var responseBody = await response.Content.ReadAsStringAsync();
        var record = JsonConvert.DeserializeObject<PostRecord>(responseBody);

        return record;
    }

    public async Task<PostRecord?> UpdateRecord(PostRecord postRecord)
    {
        var response = await _client.PutAsJsonAsync($"posts", postRecord);
        var responseBody = await response.Content.ReadAsStringAsync();
        var record = JsonConvert.DeserializeObject<PostRecord>(responseBody);

        return record;
    }

    public async Task<PostRecord?> DeleteRecord(int id)
    {
        var response = await _client.DeleteFromJsonAsync<PostRecord>($"posts/{id}");
        return response;
    }
}