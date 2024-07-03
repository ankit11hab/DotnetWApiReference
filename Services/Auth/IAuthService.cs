namespace Blog.Api;

public interface IAuthService
{
    Task<Person?> GetPersonByEmail(string email);
    Task AddUser(Person person);
    string GenerateJwtToken(Person person);
}
