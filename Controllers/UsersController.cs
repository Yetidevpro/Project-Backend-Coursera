using Microsoft.AspNetCore.Mvc;
using Project_Backend.Models;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IJsonFileRepository<User> _userRepository;
    private readonly IJsonFileRepository<Book> _bookRepository;

    public UsersController(IJsonFileRepository<User> userRepository, IJsonFileRepository<Book> bookRepository)
    {
        _userRepository = userRepository;
        _bookRepository = bookRepository;
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        var users = _userRepository.GetAll();
        var userList = users.Select(u => new
        {
            u.Name,
            u.Email,
            u.BirthDay,
            Books = u.Books.Select(b => b.Name)
        });
        return Ok(userList);
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] User user)
    {
        var users = _userRepository.GetAll();
        user.Id = Guid.NewGuid().ToString();
        users.Add(user);
        _userRepository.SaveAll(users);
        return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public IActionResult EditUser(string id, [FromBody] User updatedUser)
    {
        var users = _userRepository.GetAll();
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user == null) return NotFound();

        user.Name = updatedUser.Name;
        user.Email = updatedUser.Email;
        user.BirthDay = updatedUser.BirthDay;
        user.Books = updatedUser.Books;

        _userRepository.SaveAll(users);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(string id)
    {
        var users = _userRepository.GetAll();
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user == null) return NotFound();

        users.Remove(user);
        _userRepository.SaveAll(users);
        return NoContent();
    }

    [HttpPost("{userId}/books")]
    public IActionResult AddBookToUser(string userId, [FromBody] Book book)
    {
        var users = _userRepository.GetAll();
        var books = _bookRepository.GetAll();
        var user = users.FirstOrDefault(u => u.Id == userId);
        if (user == null) return NotFound();

        Book existingBook;

        if (!string.IsNullOrEmpty(book.Id))
        {
            existingBook = books.FirstOrDefault(b => b.Id == book.Id);
        }
        else
        {
            existingBook = books.FirstOrDefault(b => b.Name == book.Name);
        }

        if (existingBook == null || existingBook.Count == 0) return BadRequest("Book is not available");

        existingBook.Count--;
        user.Books.Add(existingBook);
        _bookRepository.SaveAll(books);
        _userRepository.SaveAll(users);
        return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
    }


    [HttpDelete("{userId}/books/{bookId}")]
    public IActionResult DeleteBookFromUser(string userId, string bookId)
    {
        var users = _userRepository.GetAll();
        var books = _bookRepository.GetAll();
        var user = users.FirstOrDefault(u => u.Id == userId);
        if (user == null) return NotFound();

        var book = user.Books.FirstOrDefault(b => b.Id == bookId);
        if (book == null) return NotFound();

        user.Books.Remove(book);
        var existingBook = books.FirstOrDefault(b => b.Id == book.Id);
        if (existingBook != null)
        {
            existingBook.Count++;
            _bookRepository.SaveAll(books);
        }

        _userRepository.SaveAll(users);
        return NoContent();
    }
}
