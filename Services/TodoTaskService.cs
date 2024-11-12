using Microsoft.EntityFrameworkCore;
using TodoWebApiTransferMate.Data;
using TodoWebApiTransferMate.Models;
using TodoWebApiTransferMate.Models.DTOs;
using TodoWebApiTransferMate.Models.Entities;

namespace TodoWebApiTransferMate.Services;

public class TodoTaskService : ITodoTaskService
{
    private readonly TodoDbContext _context;
    private readonly ICurrentTimeProvider _currentTimeProvider;

    public TodoTaskService(TodoDbContext context, ICurrentTimeProvider currentTimeProvider)
    {
        _context = context;
        _currentTimeProvider = currentTimeProvider;
    }

    public TodoTaskDTO TodoTaskToDTO(TodoTask todoTask)
    {
        return new TodoTaskDTO()
        {
            Id = todoTask.Id, Title = todoTask.Title, DueDate = todoTask.DueDate, State = todoTask.State,
        };
    }

    public async Task<IEnumerable<TodoTaskDTO>> GetAllPendingTasksAsync()
    {
        var pendingTasks = await _context.TodoTasks
            .Where(x => x.State != TodoTaskState.Completed 
                        && (x.DueDate == null || x.DueDate > _currentTimeProvider.GetCurrentTime()))
            .ToListAsync();

        return pendingTasks.Select(TodoTaskToDTO);
    }

    public async Task<IEnumerable<TodoTaskDTO>> GetAllOverdueTasksAsync()
    {
        var overdueTasks = await _context.TodoTasks
            .Where(x => x.State != TodoTaskState.Completed && x.DueDate != null && x.DueDate < _currentTimeProvider.GetCurrentTime())
            .ToListAsync();

        return overdueTasks.Select(TodoTaskToDTO);
    }

    public async Task<TodoTaskDTO?> GetTodoTaskAsync(int id)
    {
        var task = await _context.TodoTasks.FindAsync(id);

        return (task != null ? TodoTaskToDTO(task) : null);
    }

    public async Task<TodoTaskDTO> CreateTodoTaskAsync(CreateTodoTaskDTO model)
    {
        var todoTask = new TodoTask() { Title = model.Title, DueDate = model.DueDate, };

        await _context.TodoTasks.AddAsync(todoTask);
        await _context.SaveChangesAsync();

        return TodoTaskToDTO(todoTask);
    }

    public async Task<bool> EditTodoTaskAsync(int id, EditTodoTaskDTO model)
    {
        var task = await _context.TodoTasks.FindAsync(id);

        if (task == null)
        {
            return false;
        }

        if (model.Title != null) task.Title = model.Title;
        if (model.DueDate != null) task.DueDate = model.DueDate;
        if (model.State != null) task.State = model.State.Value;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<TodoTaskDTO>> GetAllTodoTasksAsync()
    {
        return (await _context.TodoTasks.ToListAsync()).Select(TodoTaskToDTO);
    }
}