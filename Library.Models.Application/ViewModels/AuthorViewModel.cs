namespace Library.Models.Application.ViewModels;

public class AuthorViewModel
{
    public string Name { get; set; }
    public DateTime BirthDay { get; set; }
    public List<BookViewModel>? Books { get; set; }
}