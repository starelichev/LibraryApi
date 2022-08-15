namespace Library.Models.Application.ViewModels;

public class ReaderWithBooks
{
    public string Name { get; set; }

    public string Email { get; set; }
    
    public List<BookViewModel> BooksOnHand { get; set; }
}