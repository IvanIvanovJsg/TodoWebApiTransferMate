using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoWebApiTransferMate.Data;
using TodoWebApiTransferMate.Models;
using TodoWebApiTransferMate.Models.DTOs;
using TodoWebApiTransferMate.Models.Entities;
using TodoWebApiTransferMate.Services;

namespace TodoWebApiTransferMate.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoTaskController : ControllerBase
{
    private readonly TodoDbContext _context;
    private readonly ITodoTaskService _todoTaskService;

    public TodoTaskController(TodoDbContext context, ITodoTaskService todoTaskService)
    {
        _context = context;
        _todoTaskService = todoTaskService;
    }

    [HttpGet]
    public async Task<IEnumerable<TodoTaskDTO>> GetAllTodoTasks()
    {
        return (await _context.TodoTasks.ToListAsync()).Select(x => _todoTaskService.TodoTaskToDTO(x));
    }
    
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTodoTask(int id)
    {
        var task = await _context.TodoTasks.FirstOrDefaultAsync(x => x.Id == id);

        if (task == null)
        {
            return NotFound("Task not found.");
        }
            
        return Ok(_todoTaskService.TodoTaskToDTO(task));
    }

    [HttpPost]
    public async Task<IActionResult> CreateTodoTask(CreateTodoTaskDTO model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var todoTask = new TodoTask()
        {
            Title = model.Title,
            DueDate = model.DueDate,
        };

        await _context.TodoTasks.AddAsync(todoTask);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetTodoTask), new {id = todoTask.Id }, _todoTaskService.TodoTaskToDTO(todoTask));
    }

}