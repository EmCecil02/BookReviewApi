using BookReviewApi.Data;
using BookReviewApi.Models;
using BookReviewApi.Services.Implementations;
using Microsoft.EntityFrameworkCore;

namespace BookReviewApi.Tests.Services;

public class ReviewServiceTests
{
    private AppDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task CreateReviewAsync_ShouldAddReview()
    {
        // Arrange
        var context = GetDbContext();

        var user = new User
        {
            Username = "emma",
            Email = "emma@test.com"
        };

        var book = new Book
        {
            Title = "Dune",
            Author = "Frank Herbert"
        };

        context.Users.Add(user);
        context.Books.Add(book);

        await context.SaveChangesAsync();

        var service = new ReviewService(context);

        var review = new Review
        {
            Content = "Amazing book",
            Rating = 5,
            UserId = user.UserId,
            BookId = book.BookId
        };

        // Act
        var result = await service.CreateReviewAsync(review);

        // Assert
        Assert.NotNull(result);

        Assert.Equal("Amazing book", result.Content);

        Assert.Equal(1, context.Reviews.Count());
    }

    [Fact]
    public async Task GetAllReviewsAsync_ShouldReturnAllReviews()
    {
        // Arrange
        var context = GetDbContext();

        var user = new User
        {
            Username = "emma",
            Email = "emma@test.com"
        };

        var book = new Book
        {
            Title = "Dune",
            Author = "Frank Herbert"
        };

        context.Users.Add(user);
        context.Books.Add(book);

        await context.SaveChangesAsync();

        context.Reviews.AddRange(
            new Review
            {
                Content = "Review 1",
                Rating = 5,
                UserId = user.UserId,
                BookId = book.BookId
            },
            new Review
            {
                Content = "Review 2",
                Rating = 4,
                UserId = user.UserId,
                BookId = book.BookId
            }
        );

        await context.SaveChangesAsync();

        var service = new ReviewService(context);

        // Act
        var result = await service.GetAllReviewsAsync();

        // Assert
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetReviewByIdAsync_ShouldReturnCorrectReview()
    {
        // Arrange
        var context = GetDbContext();

        var user = new User
        {
            Username = "emma",
            Email = "emma@test.com"
        };

        var book = new Book
        {
            Title = "Dune",
            Author = "Frank Herbert"
        };

        context.Users.Add(user);
        context.Books.Add(book);

        await context.SaveChangesAsync();

        var review = new Review
        {
            Content = "Excellent",
            Rating = 5,
            UserId = user.UserId,
            BookId = book.BookId
        };

        context.Reviews.Add(review);

        await context.SaveChangesAsync();

        var service = new ReviewService(context);

        // Act
        var result = await service.GetReviewByIdAsync(review.ReviewId);

        // Assert
        Assert.NotNull(result);

        Assert.Equal("Excellent", result!.Content);
    }

    [Fact]
    public async Task DeleteReviewAsync_ShouldRemoveReview()
    {
        // Arrange
        var context = GetDbContext();

        var user = new User
        {
            Username = "emma",
            Email = "emma@test.com"
        };

        var book = new Book
        {
            Title = "Dune",
            Author = "Frank Herbert"
        };

        context.Users.Add(user);
        context.Books.Add(book);

        await context.SaveChangesAsync();

        var review = new Review
        {
            Content = "Delete this",
            Rating = 3,
            UserId = user.UserId,
            BookId = book.BookId
        };

        context.Reviews.Add(review);

        await context.SaveChangesAsync();

        var service = new ReviewService(context);

        // Act
        var result = await service.DeleteReviewAsync(review.ReviewId);

        // Assert
        Assert.True(result);

        Assert.Equal(0, context.Reviews.Count());
    }
}