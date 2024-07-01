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

    [HttpPut]
    public async Task<IActionResult> UpdatePerson([FromBody] UpdatePersonRequest req)
    {
        var person = new Person() { Id = req.Id, Name = req.Name };
        _dbContext.Persons.Update(person);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson([FromRoute] int id)
    {
        var person = await _dbContext.Persons.FirstOrDefaultAsync(p => p.Id == id);
        if(person is null) return NotFound();
        _dbContext.Persons.Remove(person);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}
