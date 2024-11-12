using TodoWebApiTransferMate.Models.DTOs;
using TodoWebApiTransferMate.Models.Entities;

namespace TodoWebApiTransferMate.Services;

public interface ITodoTaskService
{
    TodoTaskDTO TodoTaskToDTO(TodoTask todoTask);
}