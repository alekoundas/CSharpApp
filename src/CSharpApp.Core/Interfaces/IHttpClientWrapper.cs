namespace CSharpApp.Core.Interfaces;

public interface IHttpClientWrapper
{
    Task<ReadOnlyCollection<TEntity>?> GetAllRecords<TEntity>(string url);
    Task<TEntity?> GetRecordById<TEntity>(string url);
    Task<TEntity?> InsertRecord<TEntity>(string url, TEntity record);
    Task<TEntity?> UpdateRecord<TEntity>(string url, TEntity record);
    Task<TEntity?> DeleteRecord<TEntity>(string url);
}