using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Api;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService) {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<LoginResponse> LoginUser([FromBody] LoginRequest req)
    {
        var user = await _authService.GetPersonByEmail(req.Email);
        if(user == null) return new LoginResponse(false, "User not found");

        bool checkPassword = BCrypt.Net.BCrypt.Verify(req.Password, user.Password);
        if(!checkPassword)
            return new LoginResponse(false, "Invalid Credentials");
        return new LoginResponse(true, "Login successful", _authService.GenerateJwtToken(user));
    }

    [HttpPost("register")]
    public async Task<RegisterResponse> RegisterUser([FromBody] RegisterRequest req)
    {
        var checkUser = await _authService.GetPersonByEmail(req.Email);
        if(checkUser != null) return new RegisterResponse(false, "User already exists");
        var user = req.toPersonFromRegisterRequest();
        await _authService.AddUser(user);
        return new RegisterResponse(true, "Registration Successful");
    }
}
