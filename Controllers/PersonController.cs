using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api;

[Route("api/person")]
[ApiController]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;
    public PersonController(IPersonService personService) {
        _personService = personService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPersons()
    {
        var persons = await _personService.GetAllAsync();
        var personsRes = persons.Select(p => p.toPersonSummaryResponse());
        return Ok(personsRes);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePerson([FromBody] CreatePersonRequest req)
    {
        var person = req.toPersonFromCreateRequest();
        await _personService.CreateAsync(person);
        return Created();
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePerson([FromBody] UpdatePersonRequest req)
    {
        var person = new Person() { Id = req.Id, Name = req.Name };
        await _personService.UpdateAsync(person);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson([FromRoute] int id)
    {
        var person = await _personService.GetByIdAsync(id);
        if(person is null) return NotFound();
        await _personService.DeleteAsync(person);
        return Ok();
    }
}
