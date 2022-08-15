namespace Library.Models.Application.ViewModels;

public class BookViewModel
{
    public string Name { get; set; }
    public bool Available { get; set; }
    public Guid AuthorId { get; set; }
    public AuthorViewModel? Author { get; set; }
}