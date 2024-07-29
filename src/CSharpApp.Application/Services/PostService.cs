namespace CSharpApp.Application.Services;

public class PostService : IPostService
{
    private readonly ILogger<PostService> _logger;
    private readonly HttpClient _client;

    private readonly string? _baseUrl;

    public PostService(ILogger<PostService> logger,
        IConfiguration configuration)
    {
        _logger = logger;
        _client = new HttpClient();
        _baseUrl = configuration["BaseUrl"];
    }

    public async Task<PostRecord?> GetByRecordId(int id)
    {
        _client.BaseAddress = new Uri(_baseUrl!);
        var response = await _client.GetFromJsonAsync<PostRecord>($"posts/{id}");

        return response;
    }

    public async Task<ReadOnlyCollection<PostRecord>> GetAllRecords()
    {
        _client.BaseAddress = new Uri(_baseUrl!);
        var response = await _client.GetFromJsonAsync<List<PostRecord>>($"posts");

        return response!.AsReadOnly();
    }

    public async Task<bool> InsertRecord(PostRecord postRecord)
    {
        _client.BaseAddress = new Uri(_baseUrl!);
        var response = await _client.PostAsJsonAsync($"posts", postRecord);

        return response.IsSuccessStatusCode;
    }

    public async Task<PostRecord?> DeleteRecord(int id)
    {
        _client.BaseAddress = new Uri(_baseUrl!);
        var response = await _client.DeleteFromJsonAsync<PostRecord>($"posts/{id}");

        return response;
    }
}