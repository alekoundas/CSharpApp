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
        var response = await _client.GetAsync($"posts/{id}");
        if (!response.IsSuccessStatusCode)
            return null;

        var responseRecord = await response.Content.ReadFromJsonAsync<PostRecord?>();

        return responseRecord;
    }

    public async Task<ReadOnlyCollection<PostRecord>> GetAllRecords()
    {
        var response = await _client.GetFromJsonAsync<List<PostRecord>>($"posts");
        return response!.AsReadOnly();
    }

    public async Task<PostRecord?> InsertRecord(PostRecord postRecord)
    {
        var response = await _client.PostAsJsonAsync($"posts", postRecord);
        if(!response.IsSuccessStatusCode)
            return null;

        var responseRecord = await response.Content.ReadFromJsonAsync<PostRecord?>();

        return responseRecord;
    }

    public async Task<PostRecord?> UpdateRecord(PostRecord postRecord)
    {
        var id = postRecord.Id;
        var response = await _client.PutAsJsonAsync($"posts/{id}", postRecord);
        if (!response.IsSuccessStatusCode)
            return null;

        var responseRecord = await response.Content.ReadFromJsonAsync<PostRecord?>();

        return responseRecord;
    }

    public async Task<PostRecord?> DeleteRecord(int id)
    {
        var response = await _client.DeleteFromJsonAsync<PostRecord>($"posts/{id}");
        return response;
    }
}