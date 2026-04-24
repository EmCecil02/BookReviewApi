using BookReviewApi.Models;

namespace BookReviewApi.Services.Interfaces;

public interface IReviewService
{
    Task<List<Review>> GetAllReviewsAsync();

    Task<Review?> GetReviewByIdAsync(int id);

    Task<Review> CreateReviewAsync(Review review);

    Task<bool> DeleteReviewAsync(int id);
}