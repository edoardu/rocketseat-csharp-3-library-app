using LibraryWebApp.Models.Enums;

namespace LibraryWebApp.Models;

public class Book
{
    public int? Id { get; set; }

    public required string Name { get; set; }

    public required string Author { get; set; }

    public required BookGenre Genre { get; set; }

    public required double Price { get; set; }

    public int QtyInStock { get; set; } = 0;
}