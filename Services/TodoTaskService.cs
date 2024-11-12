using TodoWebApiTransferMate.Models.DTOs;
using TodoWebApiTransferMate.Models.Entities;

namespace TodoWebApiTransferMate.Services;

public class TodoTaskService : ITodoTaskService
{
    public TodoTaskDTO TodoTaskToDTO(TodoTask todoTask)
    {
        return new TodoTaskDTO()
        {
            Id = todoTask.Id,
            Title = todoTask.Title,
            DueDate = todoTask.DueDate,
            State = todoTask.State,
        };
    }
}