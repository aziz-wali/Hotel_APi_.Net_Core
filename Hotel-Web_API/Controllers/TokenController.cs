using Hotel_Web_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hotel_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly HotelContext _context;

        public TokenController(IConfiguration config,HotelContext context)
        {
            _configuration = config;
            _context = context;
        }


        [HttpPost]
        public async Task<ActionResult>Post(Person _userData)
        {
            if (_userData != null && _userData.Email != _userData.Password != null)

            {
                var user = await GetUser(_userData.Email, _userData.Password);
                if (user != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub,_configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                        new Claim (JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                        new Claim   ("Id",user.Id.ToString()),
                        new Claim   ("FirstName",user.FirstName),
                        new Claim ("LastName",user.LastName),
                        new Claim ("Email",user.Email)
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SubKeyject"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                
                }
                else
                {
                    return BadRequest("Invalid email and password");

                }
            }
            else
            {
                return BadRequest();
            }
        }


        private async Task<Person> GetUser(string email, string password)
        {
            return await _context.People.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        private async Task<Person> CheckUserEmail(string email)
        {
            return await _context.People.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
 