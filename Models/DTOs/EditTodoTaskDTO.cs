using System.ComponentModel.DataAnnotations;

namespace TodoWebApiTransferMate.Models.DTOs;

public class EditTodoTaskDTO
{
    public String? Title { get; set; } = null;

    public DateTime? DueDate { get; set; } = null;

    public TodoTaskState? State { get; set; } = null;
}