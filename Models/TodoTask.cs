using System.ComponentModel.DataAnnotations;

namespace TodoWebApiTransferMate.Models;

public class TodoTask
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }
    
    public DateTime? DueDate { get; set; }
    
    [Required]
    public TaskState State { get; set; } = TaskState.InProgress;
}