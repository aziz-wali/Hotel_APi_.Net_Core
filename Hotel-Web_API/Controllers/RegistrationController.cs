using Hotel_Web_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace Hotel_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        public IConfiguration _configuration;
        public RegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("registration")]
        public string registration(Registration registration)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("HotelDatabase").ToString());
            SqlCommand cmd= new SqlCommand("INSERT INTO Registration(UserName,Password,Email,IsActive) VALUES('"+registration.UserName+ "', '"+registration.Password+ "', '"+registration.Email+ "', '"+registration.IsActive+ "')", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return "Data inserted";
            }
            else
            {
                return "Faild";
            }
           
        }


        [HttpPost]
        [Route("login")]
        public string login(Registration registration)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("HotelDatabase").ToString());
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Registration WHERE Email= '" + registration.Email + "' AND Password ='" + registration.Password +"'AND IsActive =1" , con);
            //con.Open();
            DataTable dt = new DataTable();
            da.Fill(dt);
            //con.Close();
            if (dt.Rows.Count > 0)
            {
                return "Data Found";
            }
            else
            {
                return "Invalid User ";
            }

        }
    }
}
