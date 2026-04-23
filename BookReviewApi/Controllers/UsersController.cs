using BookReviewApi.DTOs.User;
using BookReviewApi.Models;
using BookReviewApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserResponseDto>>> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();

        var response = users.Select(user => new UserResponseDto
        {
            UserId = user.UserId,
            Username = user.Username,
            Email = user.Email
        }).ToList();

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponseDto>> GetUserById(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        var response = new UserResponseDto
        {
            UserId = user.UserId,
            Username = user.Username,
            Email = user.Email
        };

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<UserResponseDto>> CreateUser(CreateUserDto dto)
    {
        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email
        };

        var createdUser = await _userService.CreateUserAsync(user);

        var response = new UserResponseDto
        {
            UserId = createdUser.UserId,
            Username = createdUser.Username,
            Email = createdUser.Email
        };

        return CreatedAtAction(
            nameof(GetUserById),
            new { id = createdUser.UserId },
            response
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var deleted = await _userService.DeleteUserAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}