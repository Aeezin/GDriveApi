using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DriveAPI.Models.Auth;
using DriveAPI.Models.DriveAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DriveAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration
    ) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            try
            {
                var user = new ApplicationUser { UserName = model.Username, Email = model.Email };

                var result = await userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                    return BadRequest("something went wrong." + result.Errors);

                return Ok("User registered successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var user = await userManager.FindByNameAsync(request.Username);
                if (user != null && await userManager.CheckPasswordAsync(user, request.Password))
                {
                    var jwtSection = configuration.GetSection("Jwt");
                    var key = Encoding.UTF8.GetBytes(jwtSection["key"]);

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(
                            [
                                new Claim(ClaimTypes.NameIdentifier, user.Id),
                                new Claim(ClaimTypes.Name, user.UserName),
                            ]
                        ),
                        Expires = DateTime.UtcNow.AddHours(3),
                        Issuer = jwtSection["Issuer"],
                        Audience = jwtSection["Audience"],
                        SigningCredentials = new SigningCredentials(
                            new SymmetricSecurityKey(key),
                            SecurityAlgorithms.HmacSha256Signature
                        ),
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.CreateToken(tokenDescriptor);

                    return Ok(new { Token = tokenHandler.WriteToken(token) });
                }

                return Unauthorized("Invalid credentials");
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong." + ex.Message);
            }
        }
    }
}
