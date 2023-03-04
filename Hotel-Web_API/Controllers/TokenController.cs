using Hotel_Web_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.IdentityModel.Tokens.Jwt;
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

        

    }
}
