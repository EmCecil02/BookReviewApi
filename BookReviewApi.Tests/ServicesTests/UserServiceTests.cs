using BookReviewApi.Data;
using BookReviewApi.Models;
using BookReviewApi.Services.Implementations;
using Microsoft.EntityFrameworkCore;

namespace BookReviewApi.Tests.Services;

public class UserServiceTests
{
    private AppDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task CreateUserAsync_ShouldAddUser()
    {
        // Arrange
        var context = GetDbContext();

        var service = new UserService(context);

        var user = new User
        {
            Username = "emma",
            Email = "emma@example.com"
        };

        // Act
        var result = await service.CreateUserAsync(user);

        // Assert
        Assert.NotNull(result);

        Assert.Equal("emma", result.Username);

        Assert.Equal(1, context.Users.Count());
    }

    [Fact]
    public async Task GetAllUsersAsync_ShouldReturnAllUsers()
    {
        // Arrange
        var context = GetDbContext();

        context.Users.AddRange(
            new User
            {
                Username = "user1",
                Email = "user1@test.com"
            },
            new User
            {
                Username = "user2",
                Email = "user2@test.com"
            }
        );

        await context.SaveChangesAsync();

        var service = new UserService(context);

        // Act
        var result = await service.GetAllUsersAsync();

        // Assert
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetUserByIdAsync_ShouldReturnCorrectUser()
    {
        // Arrange
        var context = GetDbContext();

        var user = new User
        {
            Username = "emma",
            Email = "emma@example.com"
        };

        context.Users.Add(user);

        await context.SaveChangesAsync();

        var service = new UserService(context);

        // Act
        var result = await service.GetUserByIdAsync(user.UserId);

        // Assert
        Assert.NotNull(result);

        Assert.Equal("emma", result!.Username);
    }

    [Fact]
    public async Task DeleteUserAsync_ShouldRemoveUser()
    {
        // Arrange
        var context = GetDbContext();

        var user = new User
        {
            Username = "deleteUser",
            Email = "delete@test.com"
        };

        context.Users.Add(user);

        await context.SaveChangesAsync();

        var service = new UserService(context);

        // Act
        var result = await service.DeleteUserAsync(user.UserId);

        // Assert
        Assert.True(result);

        Assert.Equal(0, context.Users.Count());
    }
}