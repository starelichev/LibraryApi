using AutoMapper;
using Library.Domain.Application;
using Library.Domain.Application.Models;
using Library.Models.Application.Common;
using Library.Models.Application.Requests;
using Library.Models.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApiApplication.Controllers;

public class HistoryController : ApiController
{
    private readonly LibraryDbContext LibraryDbContext;
    private readonly IMapper Mapper;

    public HistoryController(LibraryDbContext libraryDbContext, IMapper mapper)
        => (LibraryDbContext, Mapper) = (libraryDbContext, mapper);
    
    [HttpPut("[action]")]
    public async Task<ActionResult<Result>> CreateRecord(CreateHistoryRequest request)
    {
        LibraryDbContext.Histories.Add(new History()
        {
            ReaderId = request.ReaderId,
            BookId = request.BookId,
            Returned = false,
            StartDate = DateTime.Now.ToUniversalTime(),
            EndDate = null,
        });
        await LibraryDbContext.SaveChangesAsync();
        return Ok(Result.Ok());
    }
    
    [HttpGet("[action]")]
    public async Task<ActionResult<Result<ReaderWithBooks>>> GetBooksOnHand(Guid id)
    {
        var reader = await LibraryDbContext.Readers.FirstOrDefaultAsync(r => r.Id == id);
        if (reader == null)
            return Ok(Result.Fail("Читатель не найден"));
        var bookIds = await LibraryDbContext.Histories.Where(h => h.ReaderId == id && !h.Returned)
            .Select(h => h.BookId)
            .ToListAsync();
        var books = await LibraryDbContext.Books.Where(b => bookIds.Contains(b.Id)).ToListAsync();
       
        var readerWithBooks = new ReaderWithBooks()
        {
            Email = reader.Email,
            Name = reader.Name,
            BooksOnHand = books.ConvertAll(b => Mapper.Map<BookViewModel>(b)),
        };
        return Result.Ok(readerWithBooks);
    }
}