using LibraryWebApp.Models;

public class BookService
{
    private readonly List<Book> _books = new();

    public List<Book> GetAll() => _books;

    public Book? GetBookById(int id) => _books.FirstOrDefault(b => b.Id == id);

    public void AddBook(Book book) => _books.Add(book);

    public void UpdateBookAtIndex(Book book)
    {
        var index = _books.FindIndex(b => b.Id == book.Id);

        if (index == -1)
        {
            return;
        }

        _books[index] = book;
    }

    public void RemoveBook(int id) => _books.RemoveAll(b => b.Id == id);
}