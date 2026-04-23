namespace BookReviewApi.Models;

public class Review
{
    public int ReviewId { get; set; }

    public string Content { get; set; } = string.Empty;

    public int Rating { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Foreign Keys
    public int UserId { get; set; }

    public int BookId { get; set; }

    // Navigation Properties
    public User User { get; set; } = null!;

    public Book Book { get; set; } = null!;
}