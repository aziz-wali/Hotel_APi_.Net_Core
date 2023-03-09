using Hotel_Web_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        [HttpGet]
        [Route("GetData")]
        public string GetData()
        {
            return "get data function ";
        }

        [Authorize]
        [HttpGet]
        [Route("GetDataa")]
        public string GetDataa()
        {
            return "Authenticated JWT ";
        }
    }
}
