using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Todo.Core.ErrorModels;
using Todo.Core.RequestOptions;
using Todo.Service.DataTransferObjects;
using Todo.Service.IService;

namespace Todo.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    private readonly IServiceManager serviceManager;

    public TodoController(IServiceManager serviceManager)
    {
        this.serviceManager = serviceManager;
    }

    /// <summary>
    /// Retrieves all todo items
    /// </summary>
    /// <returns>List of todo items</returns>
    /// <response code="200">Returns the list of todos</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TodoDto>>> GetAllTodo([FromQuery] RequestParameter requestParamter)
    {
        var todoDtos = await serviceManager.Todo.GetAllTodoAsync(requestParamter);
        return Ok(todoDtos);
    }


    /// <summary>
    /// Retrieves a specific todo item by ID
    /// </summary>
    /// <param name="id">The ID of the todo item</param>
    /// <returns>The requested todo item</returns>
    /// <response code="200">Returns the requested todo item</response>
    /// <response code="404">If the todo item is not found</response>
    [HttpGet("{id:guid}" , Name = "GetTodoById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetails) , StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TodoDto>> GetTodoById(Guid id)
    {
        var todoDto = await serviceManager.Todo.GetTodoByIdAsync(id , false);

        return Ok(todoDto);
    }


    /// <summary>
    /// Creates a new todo item
    /// </summary>
    /// <param name="todoForCreating">The todo item to create</param>
    /// <returns>The newly created todo item</returns>
    /// <response code="201">Returns the newly created todo item</response>
    /// <response code="400">If the request data is invalid</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails) , StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TodoDto?>> CreateTodo(TodoForCreatinDto todoForCreatin)
    {
        var todoDto = await serviceManager.Todo.CreateTodoAsync(todoForCreatin);

        return CreatedAtAction("GetTodoById" , new { id = todoDto.Id } , todoDto);
    }


    /// <summary>
    /// Updates an existing todo item
    /// </summary>
    /// <param name="id">The ID of the todo item to update</param>
    /// <param name="todoForUpdating">The updated todo data</param>
    /// <returns>No content</returns>
    /// <response code="204">If the update was successful</response>
    /// <response code="400">If the request data is invalid</response>
    /// <response code="404">If the todo item is not found</response>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorDetails) , StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails) , StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UpdateTodo(
        TodoForUpdatingDto todoForUpdating , Guid id
    )
    {
        await serviceManager.Todo.UpdateTodoAsync(todoForUpdating , id , true);

        return NoContent();
    }


    /// <summary>
    /// Partially updates a todo item
    /// </summary>
    /// <param name="id">The ID of the todo item to update</param>
    /// <param name="patchDocument">JSON Patch document containing the operations to perform</param>
    /// <returns>No content</returns>
    /// <response code="204">If the update was successful</response>
    /// <response code="400">If the patch document is invalid</response>
    /// <response code="404">If the todo item is not found</response>
    [HttpPatch("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails) , StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails) , StatusCodes.Status404NotFound)]
    public async Task<ActionResult> PartialUpdateTodo(
        JsonPatchDocument<Core.Entities.Todo> patchDocument , Guid id
    )
    {
        var todoDto = await serviceManager.Todo.GetTodoEntityByIdAsync(id , true);
        patchDocument.ApplyTo(todoDto , ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await serviceManager.Todo.SaveChanges();

        return NoContent();
    }


    /// <summary>
    /// Deletes a todo item
    /// </summary>
    /// <param name="id">The ID of the todo item to delete</param>
    /// <returns>No content</returns>
    /// <response code="204">If the deletion was successful</response>
    /// <response code="404">If the todo item is not found</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorDetails) , StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteTodo(Guid id)
    {
        await serviceManager.Todo.DeleteTodoAsync(id , true);

        return NoContent();
    }
}
