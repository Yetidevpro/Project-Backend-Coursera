using Microsoft.AspNetCore.Mvc;
using Project_Backend.Models;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IJsonFileRepository<Book> _bookRepository;

    public BooksController(IJsonFileRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }

    [HttpGet]
    public IActionResult GetBooks()
    {
        var books = _bookRepository.GetAll();
        var bookList = books.Select(b => new { b.Id, b.Name, b.Count, b.ReturnDate });
        return Ok(bookList);
    }

    [HttpPost]
    public IActionResult AddBook([FromBody] Book book)
    {
        var books = _bookRepository.GetAll();
        book.Id = Guid.NewGuid().ToString();
        books.Add(book);
        _bookRepository.SaveAll(books);
        return CreatedAtAction(nameof(GetBooks), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    public IActionResult EditBook(string id, [FromBody] Book updatedBook)
    {
        var books = _bookRepository.GetAll();
        var book = books.FirstOrDefault(b => b.Id == id);
        if (book == null) return NotFound();

        book.Name = updatedBook.Name;
        book.Count = updatedBook.Count;
        book.ReturnDate = updatedBook.ReturnDate;

        _bookRepository.SaveAll(books);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(string id)
    {
        var books = _bookRepository.GetAll();
        var book = books.FirstOrDefault(b => b.Id == id);
        if (book == null) return NotFound();

        books.Remove(book);
        _bookRepository.SaveAll(books);
        return NoContent();
    }
}
