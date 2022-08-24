using BCEPtest.Data;
using BCEPtest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BCEPtest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly UserAPIDBcontext dbContext;
        public UsersController(UserAPIDBcontext dbContext)
        {
            this.dbContext = dbContext;
        }
        //--
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            return Ok(await dbContext.Users.ToListAsync());
            
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserRequest addUserRequest)
        {
            var user = new User()
            {
                id = Guid.NewGuid(),
                Email = addUserRequest.Email,
                FirstName = addUserRequest.FirstName,
                LastName = addUserRequest.LastName,
                Dateob = addUserRequest.Dateob,
                Password = addUserRequest.Password,
                Conuntry = addUserRequest.Conuntry,

            };
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id,UpdateUserRequest updateUserRequest)
        {
            var user = await dbContext.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();

            }
            else
            {
                user.Email = updateUserRequest.Email;
                user.FirstName = updateUserRequest.FirstName;
                user.LastName = updateUserRequest.LastName;
                user.Dateob = updateUserRequest.Dateob;
                user.Password = updateUserRequest.Password;
                user.Conuntry = updateUserRequest.Conuntry;

                await dbContext.SaveChangesAsync();
                return Ok(user);
            }

        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var user = await dbContext.Users.FindAsync(id);

            if (user != null)
            {
                dbContext.Users.Remove(user);
                await dbContext.SaveChangesAsync();
                return Ok(user);
            }
            return NotFound();
        }

    }
        //--
}
