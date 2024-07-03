using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllPersons()
    {
        var persons = await _personService.GetAllAsync();
        var personsRes = persons.Select(p => p.toPersonSummaryResponse());
        return Ok(personsRes);
    }

    [HttpGet("{id}")]
    [Authorize] // Access to every authorized users
    public async Task<IActionResult> GetPersonById([FromRoute] int id)
    {
        var person = await _personService.GetByIdAsync(id);
        if(person is null) return NotFound();
        var personRes = person.toPersonDetailResponse();
        return Ok(personRes);
    }

    [HttpPost("photo")]
    public async Task<IActionResult> CreatePersonPhoto([FromBody] CreatePersonPhotoRequest req)
    {
        var person = await _personService.GetByIdAsync(req.PersonId);
        if(person is null) return NotFound();
        var photo = req.toPersonPhotoFromCreateRequest(person);
        await _personService.AddPhotoAsync(photo);
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
