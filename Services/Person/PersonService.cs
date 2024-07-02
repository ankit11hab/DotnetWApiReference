
using Microsoft.EntityFrameworkCore;

namespace Blog.Api;

public class PersonService : IPersonService
{
    private readonly BloggingContext _dbContext;
    public PersonService(BloggingContext dbContext) {
        _dbContext = dbContext;
    }

    public async Task CreateAsync(Person person)
    {
        await _dbContext.Persons.AddAsync(person);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddPhotoAsync(PersonPhoto photo)
    {
        await _dbContext.PersonPhotos.AddAsync(photo);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Person person)
    {
        _dbContext.Persons.Remove(person);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Person>> GetAllAsync()
    {
        var persons = await _dbContext.Persons.ToListAsync();
        return persons;
    }

    public async Task<Person?> GetByIdAsync(int id)
    {
        var person = await _dbContext.Persons
                                        .Include(p => p.Photo)
                                        .FirstOrDefaultAsync(p => p.Id == id);
        return person;
    }

    public async Task UpdateAsync(Person person)
    {
        _dbContext.Persons.Update(person);
        await _dbContext.SaveChangesAsync();
    }
}
