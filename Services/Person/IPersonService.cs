namespace Blog.Api;

public interface IPersonService
{
    Task<List<Person>> GetAllAsync();
    Task<Person?> GetByIdAsync(int id);
    Task UpdateAsync(Person person);
    Task CreateAsync(Person person);
    Task DeleteAsync(Person person);
}
