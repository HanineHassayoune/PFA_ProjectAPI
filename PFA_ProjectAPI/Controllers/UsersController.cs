using API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PFA_ProjectAPI.Models.Domain;
using PFA_ProjectAPI.Models.DtoUser;

namespace PFA_ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly TBDbContext dbContext;
        public UsersController(TBDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpPost]
        [Route("Registration")]
        public IActionResult Registration(UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var objUser = dbContext.Users.FirstOrDefault(x => x.Email == userDto.Email);
            if (objUser == null)
            {
                var newUser = new User
                {
                    Name = userDto.Name,
                    Email = userDto.Email,
                    Password = userDto.Password,
                    Role= userDto.Role,
                };

                dbContext.Users.Add(newUser);
                dbContext.SaveChanges();

                return Ok(new
                {
                   

                    email=newUser.Email,
                    role=newUser.Role,
                    id=newUser.Id,
                });
            }
            else
            {
                return BadRequest(new { message = "User already exists with the same email address" });
            }
        }



        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginDto loginDto)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.Email == loginDto.Email && x.Password == loginDto.Password);
            if(user != null)
            {
                return Ok(new
                {
                    email = user.Email,
                    role = user.Role,
                    id = user.Id,
                });
               

            }
            return NoContent();
        }


        [HttpGet]
        [Route("GetUsers")]
        public IActionResult GetUsers()
        {
            return Ok(dbContext.Users.ToList());
        }


        [HttpGet]
        [Route("GetUser")]
        public IActionResult GetUserById(Guid id)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.Id == id);
            if (user != null)
                return Ok(user);
            else
                return NoContent();
        }
    }
}
