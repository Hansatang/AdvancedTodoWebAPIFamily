using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using AdvancedTodoWebAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace AdvancedTodoWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }
        
        
        [HttpGet]
        public async Task<ActionResult<User>> 
            ValidateUser([FromQuery] string? username, [FromQuery] string? password)
        {
            Console.WriteLine("Here");
            try
            {
                User user = await userService.ValidateUser(username, password);
                string productsAsJson = JsonSerializer.Serialize(user);
                Console.WriteLine("OK");
                return Ok(productsAsJson);
            }
            catch (Exception e)
            {
                Console.WriteLine(username+"h "+ password);
                return BadRequest(e.Message);
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<User>> AddUser([FromBody] User adult) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try {
                User added = await userService.AddUserAsync(adult);
                Console.WriteLine(added.UserName+"hej");
                return Created($"/{added}",added); // return newly added to-do, to get the auto generated id
            } catch (Exception e) {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}