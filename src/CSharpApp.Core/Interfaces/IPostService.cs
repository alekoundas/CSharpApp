namespace CSharpApp.Core.Interfaces;

public interface IPostService
{
    Task<PostRecord?> GetByRecordId(int id);
    Task<ReadOnlyCollection<PostRecord>> GetAllRecords();
    Task<PostRecord?> InsertRecord(PostRecord postRecord);
    Task<PostRecord?> UpdateRecord(PostRecord postRecord);
    Task<PostRecord?> DeleteRecord(int id);
}