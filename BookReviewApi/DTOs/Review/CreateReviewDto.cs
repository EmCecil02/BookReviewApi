namespace BookReviewApi.DTOs.Review;

public class CreateReviewDto
{
    public string Content { get; set; } = string.Empty;

    public int Rating { get; set; }

    public int UserId { get; set; }

    public int BookId { get; set; }
}