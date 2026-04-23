namespace BookReviewApi.Models;

public class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public int PublishedYear { get; set; }

    public string Description { get; set; } = string.Empty;

    // Navigation Properties
    public List<Review> Reviews { get; set; } = new();

    public List<BookGenre> BookGenres { get; set; } = new();
}