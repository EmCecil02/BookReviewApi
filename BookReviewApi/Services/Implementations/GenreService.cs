using BookReviewApi.Data;
using BookReviewApi.Models;
using BookReviewApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookReviewApi.Services.Implementations;

public class GenreService : IGenreService
{
    private readonly AppDbContext _context;

    public GenreService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Genre>> GetAllGenresAsync()
    {
        return await _context.Genres.ToListAsync();
    }

    public async Task<Genre?> GetGenreByIdAsync(int id)
    {
        return await _context.Genres.FindAsync(id);
    }

    public async Task<Genre> CreateGenreAsync(Genre genre)
    {
        _context.Genres.Add(genre);

        await _context.SaveChangesAsync();

        return genre;
    }

    public async Task<bool> DeleteGenreAsync(int id)
    {
        var genre = await _context.Genres.FindAsync(id);

        if (genre == null)
        {
            return false;
        }

        _context.Genres.Remove(genre);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddGenreToBookAsync(int bookId, int genreId)
    {
        var bookExists = await _context.Books.AnyAsync(b => b.BookId == bookId);

        var genreExists = await _context.Genres.AnyAsync(g => g.GenreId == genreId);

        if (!bookExists || !genreExists)
        {
            return false;
        }

        var alreadyExists = await _context.BookGenres
            .AnyAsync(bg => bg.BookId == bookId && bg.GenreId == genreId);

        if (alreadyExists)
        {
            return false;
        }

        var bookGenre = new BookGenre
        {
            BookId = bookId,
            GenreId = genreId
        };

        _context.BookGenres.Add(bookGenre);

        await _context.SaveChangesAsync();

        return true;
    }
}