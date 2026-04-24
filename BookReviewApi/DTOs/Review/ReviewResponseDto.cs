namespace BookReviewApi.DTOs.Review;

public class ReviewResponseDto
{
    public int ReviewId { get; set; }

    public string Content { get; set; } = string.Empty;

    public int Rating { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UserId { get; set; }

    public string Username { get; set; } = string.Empty;

    public int BookId { get; set; }

    public string BookTitle { get; set; } = string.Empty;
}