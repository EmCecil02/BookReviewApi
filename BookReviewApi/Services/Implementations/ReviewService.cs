using BookReviewApi.Data;
using BookReviewApi.Models;
using BookReviewApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookReviewApi.Services.Implementations;

public class ReviewService : IReviewService
{
    private readonly AppDbContext _context;

    public ReviewService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Review>> GetAllReviewsAsync()
    {
        return await _context.Reviews
            .Include(r => r.User)
            .Include(r => r.Book)
            .ToListAsync();
    }

    public async Task<Review?> GetReviewByIdAsync(int id)
    {
        return await _context.Reviews
            .Include(r => r.User)
            .Include(r => r.Book)
            .FirstOrDefaultAsync(r => r.ReviewId == id);
    }

    public async Task<Review> CreateReviewAsync(Review review)
    {
        _context.Reviews.Add(review);

        await _context.SaveChangesAsync();

        return review;
    }

    public async Task<bool> DeleteReviewAsync(int id)
    {
        var review = await _context.Reviews.FindAsync(id);

        if (review == null)
        {
            return false;
        }

        _context.Reviews.Remove(review);

        await _context.SaveChangesAsync();

        return true;
    }
}