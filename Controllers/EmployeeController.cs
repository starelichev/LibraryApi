using AutoMapper;
using Library.Domain.Application;
using Library.Domain.Application.Models;
using Library.Models.Application.Common;
using Library.Models.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApiApplication.Controllers;

public class EmployeeController : ApiController
{
    private readonly LibraryDbContext LibraryDbContext;
    public EmployeeController(LibraryDbContext libraryDbContext) => LibraryDbContext = libraryDbContext;

    [HttpPut("[action]")]
    public async Task<ActionResult<Result>> AddEmployee(EmployeeViewModel employee)
    {
        LibraryDbContext.Employees.Add(new Employee()
        {
            Name = employee.Name,
            Login = employee.Login,
            Pass = employee.Pass,
        });
        await LibraryDbContext.SaveChangesAsync();
        return Ok(Result.Ok());
    }
}