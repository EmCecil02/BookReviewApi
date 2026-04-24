using BookReviewApi.DTOs.Review;
using BookReviewApi.Models;
using BookReviewApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewsController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ReviewResponseDto>>> GetAllReviews()
    {
        var reviews = await _reviewService.GetAllReviewsAsync();

        var response = reviews.Select(review => new ReviewResponseDto
        {
            ReviewId = review.ReviewId,
            Content = review.Content,
            Rating = review.Rating,
            CreatedAt = review.CreatedAt,
            UserId = review.UserId,
            Username = review.User.Username,
            BookId = review.BookId,
            BookTitle = review.Book.Title
        }).ToList();

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReviewResponseDto>> GetReviewById(int id)
    {
        var review = await _reviewService.GetReviewByIdAsync(id);

        if (review == null)
        {
            return NotFound();
        }

        var response = new ReviewResponseDto
        {
            ReviewId = review.ReviewId,
            Content = review.Content,
            Rating = review.Rating,
            CreatedAt = review.CreatedAt,
            UserId = review.UserId,
            Username = review.User.Username,
            BookId = review.BookId,
            BookTitle = review.Book.Title
        };

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<ReviewResponseDto>> CreateReview(CreateReviewDto dto)
    {
        var review = new Review
        {
            Content = dto.Content,
            Rating = dto.Rating,
            UserId = dto.UserId,
            BookId = dto.BookId
        };

        var createdReview = await _reviewService.CreateReviewAsync(review);

        var response = new ReviewResponseDto
        {
            ReviewId = createdReview.ReviewId,
            Content = createdReview.Content,
            Rating = createdReview.Rating,
            CreatedAt = createdReview.CreatedAt,
            UserId = createdReview.UserId,
            BookId = createdReview.BookId
        };

        return CreatedAtAction(
            nameof(GetReviewById),
            new { id = createdReview.ReviewId },
            response
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReview(int id)
    {
        var deleted = await _reviewService.DeleteReviewAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}