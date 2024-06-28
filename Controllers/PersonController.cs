using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api;

[Route("api/person")]
[ApiController]
public class PersonController : ControllerBase
{
    private readonly BloggingContext _dbContext;
    public PersonController(BloggingContext dbContext) {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPersons()
    {
        var persons = await _dbContext.Persons.ToListAsync();
        var personsRes = persons.Select(p => p.toPersonSummaryResponse());
        return Ok(personsRes);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePerson([FromBody] CreatePersonRequest req)
    {
        var person = req.toPersonFromCreateRequest();
        await _dbContext.Persons.AddAsync(person);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}
