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
public class TodosController : ControllerBase {
    private ITodosService todosService;

    public TodosController(ITodosService todosService) {
        this.todosService = todosService;
    }

    [HttpGet]
    public async Task<ActionResult<IList<Family>>> 
        GetTodos([FromQuery] int? userId, [FromQuery] bool? isCompleted) {
        try {
            IList<Family> todos = await todosService.GetTodosAsync();
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
    public async Task<ActionResult<Family>> AddTodo([FromBody] Family family) {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try {
            Family added = await todosService.AddTodoAsync(family);
            return Created($"/{added.Id}",added); // return newly added to-do, to get the auto generated id
        } catch (Exception e) {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch]
    [Route("{id:int}")]
    public async Task<ActionResult<Family>> UpdateTodo([FromBody] Family family) {
        try {
            Family updatedFamily = await todosService.UpdateAsync(family);
            return Ok(updatedFamily); 
        } catch (Exception e) {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}
}