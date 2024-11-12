namespace TodoWebApiTransferMate.Models.DTOs;

public class TodoTaskDTO
{
    public int Id { get; set; }
    
    public string Title { get; set; }

    public DateTime? DueDate { get; set; }

    public TodoTaskState State { get; set; }
    
}