namespace Library.Models.Application.ViewModels;

public class HistoryViewModel
{
    public Guid ReaderId { get; set; }
    public Guid BookId { get; set; }
    public bool Returned { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}