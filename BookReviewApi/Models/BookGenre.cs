namespace BookReviewApi.Models;

public class BookGenre
{
    // Composite Key
    public int BookId { get; set; }

    public int GenreId { get; set; }

    // Navigation Properties
    public Book Book { get; set; } = null!;

    public Genre Genre { get; set; } = null!;
}