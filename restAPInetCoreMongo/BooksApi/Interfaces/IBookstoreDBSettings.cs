namespace BooksApi.Interfaces
{
    public interface IBookstoreDBSettings
    {
        string BooksCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}