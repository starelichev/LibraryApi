using AutoMapper;
using Library.Domain.Application;
using Library.Domain.Application.Models;
using Library.Models.Application.Common;
using Library.Models.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApiApplication.Controllers;

public class ReaderController : ApiController
{
    private readonly LibraryDbContext LibraryDbContext;
    private readonly IMapper Mapper;

    public ReaderController(LibraryDbContext libraryDbContext, IMapper mapper)
        => (LibraryDbContext, Mapper) = (libraryDbContext, mapper);

    [HttpPut("[action]")]
    public async Task<ActionResult<Result>> AddReader(ReaderViewModel reader)
    {
        LibraryDbContext.Readers.Add(new Reader()
        {
            Name = reader.Name,
            Email = reader.Email,
        });
        await LibraryDbContext.SaveChangesAsync();
        return Ok(Result.Ok());
    }
    
    [HttpGet("[action]")]
    public async Task<ActionResult<Result<List<ReaderViewModel>>>> SearchReaders(string phrase)
    {
        List<Reader> readers = await LibraryDbContext.Readers.
            Where(a => a.Name.Contains(phrase) || a.Email.Contains(phrase)).ToListAsync();
        return Result.Ok(readers.ConvertAll(a => Mapper.Map<ReaderViewModel>(a)));
    }
}