using BookReviewApi.DTOs.Genre;
using BookReviewApi.Models;
using BookReviewApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenresController : ControllerBase
{
    private readonly IGenreService _genreService;

    public GenresController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    [HttpGet]
    public async Task<ActionResult<List<GenreResponseDto>>> GetAllGenres()
    {
        var genres = await _genreService.GetAllGenresAsync();

        var response = genres.Select(g => new GenreResponseDto
        {
            GenreId = g.GenreId,
            Name = g.Name
        }).ToList();

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GenreResponseDto>> GetGenreById(int id)
    {
        var genre = await _genreService.GetGenreByIdAsync(id);

        if (genre == null)
        {
            return NotFound();
        }

        var response = new GenreResponseDto
        {
            GenreId = genre.GenreId,
            Name = genre.Name
        };

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<GenreResponseDto>> CreateGenre(CreateGenreDto dto)
    {
        var genre = new Genre
        {
            Name = dto.Name
        };

        var createdGenre = await _genreService.CreateGenreAsync(genre);

        var response = new GenreResponseDto
        {
            GenreId = createdGenre.GenreId,
            Name = createdGenre.Name
        };

        return CreatedAtAction(
            nameof(GetGenreById),
            new { id = createdGenre.GenreId },
            response
        );
    }

    [HttpPost("assign/{bookId}/{genreId}")]
    public async Task<IActionResult> AddGenreToBook(int bookId, int genreId)
    {
        var success = await _genreService.AddGenreToBookAsync(bookId, genreId);

        if (!success)
        {
            return BadRequest("Could not assign genre to book.");
        }

        return Ok("Genre assigned to book successfully.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        var deleted = await _genreService.DeleteGenreAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}