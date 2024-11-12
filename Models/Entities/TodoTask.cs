using System.ComponentModel.DataAnnotations;

namespace TodoWebApiTransferMate.Models.Entities;

public class TodoTask
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }
    
    public DateTime? DueDate { get; set; }
    
    [Required]
    public TodoTaskState State { get; set; } = TodoTaskState.InProgress;
}