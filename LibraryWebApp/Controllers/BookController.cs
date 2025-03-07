using LibraryWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Controllers;

public class BookController : LibraryWebAppBaseController
{
    private readonly BookService _bookService;

    public BookController(BookService bookService)
    {
        _bookService = bookService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Book), StatusCodes.Status201Created)]
    public IActionResult Create([FromBody] Book book)
    {
        var newBook = new Book
        {
            Id = _bookService.GetAll().Count + 1,
            Name = book.Name,
            Author = book.Author,
            Genre = book.Genre,
            Price = book.Price,
            QtyInStock = book.QtyInStock
        };

        _bookService.AddBook(newBook);
        return Created($"/api/books/{newBook.Id}", newBook);
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Book>), StatusCodes.Status200OK)]
    public IActionResult GetAllBooks()
    {
        return Ok(_bookService.GetAll());
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdateBook([FromBody] Book book)
    {
        if (!book.Id.HasValue)
        {
            return BadRequest("Book ID is required.");
        }

        var bookToUpdate = _bookService.GetBookById(book.Id.Value);

        if (bookToUpdate == null)
        {
            return NotFound();
        }

        bookToUpdate.Name = book.Name;
        bookToUpdate.Author = book.Author;
        bookToUpdate.Genre = book.Genre;
        bookToUpdate.Price = book.Price;
        bookToUpdate.QtyInStock = book.QtyInStock;

        _bookService.UpdateBookAtIndex(bookToUpdate);
        return Ok();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult RemoveBook([FromRoute] int id)
    {
        if (_bookService.GetBookById(id) == null)
        {
            return NotFound();
        }

        _bookService.RemoveBook(id);
        return Ok();
    }
}