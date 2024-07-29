namespace CSharpApp.Core.Interfaces;

public interface IPostService
{
    Task<PostRecord?> GetByRecordId(int id);
    Task<ReadOnlyCollection<PostRecord>> GetAllRecords();
    Task<bool> InsertRecord(PostRecord postRecord);
    Task<PostRecord?> DeleteRecord(int id);
}