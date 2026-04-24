using BookReviewApi.Models;

namespace BookReviewApi.Services.Interfaces;

public interface IGenreService
{
    Task<List<Genre>> GetAllGenresAsync();

    Task<Genre?> GetGenreByIdAsync(int id);

    Task<Genre> CreateGenreAsync(Genre genre);

    Task<bool> DeleteGenreAsync(int id);

    Task<bool> AddGenreToBookAsync(int bookId, int genreId);
}