using AutoMapper;
using Library.Domain.Application;
using Library.Domain.Application.Models;
using Library.Models.Application.Common;
using Library.Models.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApiApplication.Controllers;

public class BookController : ApiController
{
    private readonly LibraryDbContext LibraryDbContext;
    private readonly IMapper Mapper;
    public BookController(LibraryDbContext libraryDbContext, IMapper mapper)
    {
        LibraryDbContext = libraryDbContext;
        Mapper = mapper;
    }

    [HttpPut("[action]")]
    public async Task<ActionResult<Result>> AddBook(BookViewModel book)
    {
        LibraryDbContext.Books.Add(new Book
        {
            Name = book.Name,
            AvailableBook = book.Available,
            AuthorId = book.AuthorId
                
        });
        await LibraryDbContext.SaveChangesAsync();
        return Ok(Result.Ok());
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<Result<List<BookViewModel>>>> GetBooks()
    {
        var books = await LibraryDbContext.Books.Include(b=>b.Author).ToListAsync();

        return Ok(books.ConvertAll(a => Mapper.Map<BookViewModel>(a)));
    }
    
    [HttpGet("[action]")]
    public async Task<ActionResult<Result<List<BookViewModel>>>> SearchBooks(string phrase)
    {
        List<Book> books = await LibraryDbContext.Books.Where(a => a.Name.Contains(phrase)).ToListAsync();
        return Result.Ok(books.ConvertAll(a => Mapper.Map<BookViewModel>(a)));
    }
}