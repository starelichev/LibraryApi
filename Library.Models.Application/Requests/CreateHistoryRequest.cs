namespace Library.Models.Application.Requests;

public class CreateHistoryRequest
{
    public Guid ReaderId { get; set; }
    public Guid BookId { get; set; }
}