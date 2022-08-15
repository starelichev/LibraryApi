using AutoMapper;
using Library.Domain.Application;
using Library.Domain.Application.Models;
using Library.Models.Application.Common;
using Library.Models.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApiApplication.Controllers
{
    public class AuthorController : ApiController
    {
        private readonly LibraryDbContext LibraryDbContext;
        private readonly IMapper Mapper;

        public AuthorController(LibraryDbContext libraryDbContext, IMapper mapper)
        {
            LibraryDbContext = libraryDbContext;
            Mapper = mapper;
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<Result>> AddAuthor(AuthorViewModel author)
        {
            LibraryDbContext.Authors.Add(new Author
            {
                Name = author.Name,
                BirthDay = author.BirthDay.ToUniversalTime(),
            });
            await LibraryDbContext.SaveChangesAsync();
            return Ok(Result.Ok());
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<AuthorViewModel>>> GetAuthors()
        {
            return Ok(await LibraryDbContext.Authors.ToListAsync());
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Result<AuthorViewModel>>> GetAuthor(Guid id)
        {
            var author = await LibraryDbContext.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == id);
            if (author != null)
            {
                return Ok(Result.Ok(Mapper.Map<AuthorViewModel>(author)));
            }
            else
            {
                return Ok(Result.Fail("Автор не найден"));
            }
        }

        [HttpDelete("[action]/{Id}")]

        public async Task<ActionResult<Result>> DeleteAuthor(Guid id)
        {
            var author = await LibraryDbContext.Authors.FirstOrDefaultAsync(a => a.Id == id);

            if (author != null)
            {
                LibraryDbContext.Authors.Remove(author);
                await LibraryDbContext.SaveChangesAsync();
                return Ok(Result.Ok());
            }
            else
            {
                return Ok(Result.Fail("Такого автора не существует"));
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<Result<List<AuthorViewModel>>>> SearchAuthors(string phrase)
        {
            List<Author> authors = await LibraryDbContext.Authors.Where(a => a.Name.Contains(phrase)).ToListAsync();
            return Result.Ok(authors.ConvertAll(a => Mapper.Map<AuthorViewModel>(a)));
        }
    }
}