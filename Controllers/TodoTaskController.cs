using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoWebApiTransferMate.Data;
using TodoWebApiTransferMate.Models.DTOs;
using TodoWebApiTransferMate.Models.Entities;
using TodoWebApiTransferMate.Services;

namespace TodoWebApiTransferMate.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoTaskController : ControllerBase
{
    private readonly ITodoTaskService _todoTaskService;

    public TodoTaskController(ITodoTaskService todoTaskService)
    {
        _todoTaskService = todoTaskService;
    }

    [HttpGet]
    public async Task<IEnumerable<TodoTaskDTO>> GetAllTodoTasks()
    {
        var tasks = await _todoTaskService.GetAllTodoTasksAsync();
        return tasks;
    }

    [HttpGet("pending")]
    public async Task<IEnumerable<TodoTaskDTO>> GetPendingTodoTasks()
    {
        var pendingTasks = await _todoTaskService.GetAllPendingTasksAsync();
        
        return pendingTasks;
    }
    
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTodoTask(int id)
    {
        var task = await _todoTaskService.GetTodoTaskAsync(id);

        return (task != null ? Ok(task) : NotFound("Task not found."));
    }

    [HttpPost]
    public async Task<IActionResult> CreateTodoTask(CreateTodoTaskDTO model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var todoTask = await _todoTaskService.CreateTodoTaskAsync(model);
        
        return CreatedAtAction(nameof(GetTodoTask), new {id = todoTask.Id }, todoTask);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditTodoTask(int id, EditTodoTaskDTO model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!(await _todoTaskService.EditTodoTaskAsync(id, model)))
        {
            return NotFound("Task not found.");
        }

        return NoContent();
    }

}