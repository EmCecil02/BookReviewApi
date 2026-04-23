namespace BookReviewApi.Models;

public class Genre
{
    public int GenreId { get; set; }

    public string Name { get; set; } = string.Empty;

    // Navigation Property
    public List<BookGenre> BookGenres { get; set; } = new();
}