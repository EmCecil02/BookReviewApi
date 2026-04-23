public interface IBookService
{
    List<Book> GetAllBooks();
    Book GetBookById(int id);
    Book CreateBook(Book book);
    bool DeleteBook(int id);
    void AddGenreToBook(int bookId, int genreId);
}