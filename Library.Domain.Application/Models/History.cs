using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Domain.Application.Models;

public class History
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public Guid ReaderId { get; set; }
    public Guid BookId { get; set; }
    public bool Returned { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}