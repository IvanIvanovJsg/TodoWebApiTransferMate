using System.ComponentModel.DataAnnotations;

namespace TodoWebApiTransferMate.Models.DTOs;

public class CreateTodoTaskDTO
{
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }

    public DateTime? DueDate { get; set; } = null;
}