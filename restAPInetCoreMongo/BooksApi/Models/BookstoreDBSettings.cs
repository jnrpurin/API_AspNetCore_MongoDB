using BooksApi.Interfaces;

namespace BooksApi.Models
{
    public class BookstoreDBSettings : IBookstoreDBSettings
    {
        public string BooksCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}