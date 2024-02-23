using iStartWeb.Models;
using iStartWebAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace iStartWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var users = _context.Users.ToList();
                if(users.Count == 0)
                {
                    return NotFound();
                }
                return Ok(users);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        
        

       
        [HttpPost]
        public ActionResult Post([FromBody] User value)
        {

            try
            {
                _context.Users.Add(value);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public ActionResult SignIn([FromBody] User value)
        {

            try
            {
                User user = _context.Users
    .FromSqlInterpolated($@"SELECT * FROM Users 
                            WHERE Email = {value.Email} AND Password = {value.Password}")
    .FirstOrDefault();

                if (user != null)
                {
                    return Ok();
                    
                }
                return BadRequest();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

       
    }
}
