namespace CSharpApp.Core.Interfaces;

public interface IHttpClientFactory
{
    HttpClient CreateHttpClient();
}