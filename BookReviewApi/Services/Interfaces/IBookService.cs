using BookReviewApi.Models;

namespace BookReviewApi.Services.Interfaces;

public interface IBookService
{
    Task<List<Book>> GetAllBooksAsync();

    Task<Book?> GetBookByIdAsync(int id);

    Task<Book> CreateBookAsync(Book book);

    Task<bool> DeleteBookAsync(int id);
}