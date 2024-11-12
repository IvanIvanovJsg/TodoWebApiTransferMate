using TodoWebApiTransferMate.Models.DTOs;
using TodoWebApiTransferMate.Models.Entities;

namespace TodoWebApiTransferMate.Services;

public interface ITodoTaskService
{
    Task<IEnumerable<TodoTaskDTO>> GetAllTodoTasksAsync();
    Task<TodoTaskDTO?> GetTodoTaskAsync(int id);
    Task<TodoTaskDTO> CreateTodoTaskAsync(CreateTodoTaskDTO model);
    Task<bool> EditTodoTaskAsync(int id, EditTodoTaskDTO model);
    TodoTaskDTO TodoTaskToDTO(TodoTask todoTask);
    Task<IEnumerable<TodoTaskDTO>> GetAllPendingTasksAsync();
    Task<IEnumerable<TodoTaskDTO>> GetAllOverdueTasksAsync();
}