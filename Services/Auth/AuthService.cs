
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Api;

public class AuthService : IAuthService
{
    private readonly BloggingContext _dbContext;
    private readonly IConfiguration _config;
    private readonly IPersonService _personService;
    public AuthService(BloggingContext dbContext, IConfiguration config, IPersonService personService) {
        _dbContext = dbContext;
        _config = config;
        _personService = personService;
    }

    public string GenerateJwtToken(Person person)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, person.Id.ToString()),
            new Claim(ClaimTypes.Name, person.Name!),
            new Claim(ClaimTypes.Email, person.Email!)
        };
        foreach(var role in person.Roles) {
            claims.Add(new Claim(ClaimTypes.Role, role.Id));
        }
        var token = new JwtSecurityToken(
            issuer: _config["JwtSettings:Issuer"],
            audience: _config["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(5),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<Person?> GetPersonByEmail(string email)
    {
        var user = await _dbContext.Persons
                                    .Include(p => p.Roles)
                                    .FirstOrDefaultAsync(u => u.Email == email);
        return user;
    }

    public async Task AddUser(Person person)
    {
        await _dbContext.Persons.AddAsync(person);
        await _dbContext.SaveChangesAsync();
    }
}
