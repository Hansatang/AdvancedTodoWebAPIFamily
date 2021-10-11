using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AdvancedTodoWebAPI.Data;

using Microsoft.AspNetCore.Mvc;
using Models;

namespace AdvancedTodoWebAPI.Controllers {
[ApiController]
[Route("[controller]")]
public class AdultController : ControllerBase {
    private ITodosService todosService;

    public AdultController(ITodosService todosService) {
        this.todosService = todosService;
    }

    [HttpGet]
    public async Task<ActionResult<IList<Adult>>> 
        GetTodos([FromQuery] int? userId, [FromQuery] bool? isCompleted) {
        try {
            IList<Adult> todos = await todosService.GetTodosAsync();
            string productsAsJson = JsonSerializer.Serialize(todos);
            return Ok(productsAsJson);
        } catch (Exception e) {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult> DeleteTodo([FromRoute] int id) {
        try {
            await todosService.RemoveTodoAsync(id);
            return Ok();
        } catch (Exception e) {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<Adult>> AddTodo([FromBody] Adult adult) {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try {
            Adult added = await todosService.AddTodoAsync(adult);
            return Created($"/{added.Id}",added); // return newly added to-do, to get the auto generated id
        } catch (Exception e) {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch]
    [Route("{id:int}")]
    public async Task<ActionResult<Adult>> UpdateTodo([FromBody] Adult adult) {
        try {
            Adult updatedFamily = await todosService.UpdateAsync(adult);
            return Ok(updatedFamily); 
        } catch (Exception e) {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}
}