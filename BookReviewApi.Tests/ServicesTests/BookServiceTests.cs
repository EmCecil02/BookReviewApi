using BookReviewApi.Data;
using BookReviewApi.Models;
using BookReviewApi.Services.Implementations;
using Microsoft.EntityFrameworkCore;

namespace BookReviewApi.Tests.Services;

public class BookServiceTests
{
    private AppDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task CreateBookAsync_ShouldAddBook()
    {
        // Arrange
        var context = GetDbContext();

        var service = new BookService(context);

        var book = new Book
        {
            Title = "Dune",
            Author = "Frank Herbert",
            PublishedYear = 1965,
            Description = "Sci-fi novel"
        };

        // Act
        var result = await service.CreateBookAsync(book);

        // Assert
        Assert.NotNull(result);

        Assert.Equal("Dune", result.Title);

        Assert.Equal(1, context.Books.Count());
    }

    [Fact]
    public async Task GetAllBooksAsync_ShouldReturnAllBooks()
    {
        // Arrange
        var context = GetDbContext();

        context.Books.AddRange(
            new Book
            {
                Title = "Book 1",
                Author = "Author 1"
            },
            new Book
            {
                Title = "Book 2",
                Author = "Author 2"
            }
        );

        await context.SaveChangesAsync();

        var service = new BookService(context);

        // Act
        var result = await service.GetAllBooksAsync();

        // Assert
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetBookByIdAsync_ShouldReturnCorrectBook()
    {
        // Arrange
        var context = GetDbContext();

        var book = new Book
        {
            Title = "The Hobbit",
            Author = "Tolkien"
        };

        context.Books.Add(book);

        await context.SaveChangesAsync();

        var service = new BookService(context);

        // Act
        var result = await service.GetBookByIdAsync(book.BookId);

        // Assert
        Assert.NotNull(result);

        Assert.Equal("The Hobbit", result!.Title);
    }

    [Fact]
    public async Task DeleteBookAsync_ShouldRemoveBook()
    {
        // Arrange
        var context = GetDbContext();

        var book = new Book
        {
            Title = "Delete Me",
            Author = "Author"
        };

        context.Books.Add(book);

        await context.SaveChangesAsync();

        var service = new BookService(context);

        // Act
        var result = await service.DeleteBookAsync(book.BookId);

        // Assert
        Assert.True(result);

        Assert.Equal(0, context.Books.Count());
    }

    [Fact]
    public async Task DeleteBookAsync_ShouldReturnFalse_WhenBookDoesNotExist()
    {
        // Arrange
        var context = GetDbContext();

        var service = new BookService(context);

        // Act
        var result = await service.DeleteBookAsync(999);

        // Assert
        Assert.False(result);
    }
}