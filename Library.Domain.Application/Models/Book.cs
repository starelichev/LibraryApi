using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Domain.Application.Models;

public class Book
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid AuthorId { get; set; }

    public bool AvailableBook { get; set; }

    public virtual Author Author { get; set; }
}