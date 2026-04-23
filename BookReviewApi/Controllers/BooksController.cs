using BookReviewApi.DTOs.Book;
using BookReviewApi.Models;
using BookReviewApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<ActionResult<List<BookResponseDto>>> GetAllBooks()
    {
        var books = await _bookService.GetAllBooksAsync();

        var response = books.Select(book => new BookResponseDto
        {
            BookId = book.BookId,
            Title = book.Title,
            Author = book.Author,
            PublishedYear = book.PublishedYear,
            Description = book.Description
        }).ToList();

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookResponseDto>> GetBookById(int id)
    {
        var book = await _bookService.GetBookByIdAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        var response = new BookResponseDto
        {
            BookId = book.BookId,
            Title = book.Title,
            Author = book.Author,
            PublishedYear = book.PublishedYear,
            Description = book.Description
        };

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<BookResponseDto>> CreateBook(CreateBookDto dto)
    {
        var book = new Book
        {
            Title = dto.Title,
            Author = dto.Author,
            PublishedYear = dto.PublishedYear,
            Description = dto.Description
        };

        var createdBook = await _bookService.CreateBookAsync(book);

        var response = new BookResponseDto
        {
            BookId = createdBook.BookId,
            Title = createdBook.Title,
            Author = createdBook.Author,
            PublishedYear = createdBook.PublishedYear,
            Description = createdBook.Description
        };

        return CreatedAtAction(
            nameof(GetBookById),
            new { id = createdBook.BookId },
            response
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var deleted = await _bookService.DeleteBookAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}