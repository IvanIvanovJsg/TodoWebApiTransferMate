using Microsoft.EntityFrameworkCore;
using TodoWebApiTransferMate.Models.Entities;

namespace TodoWebApiTransferMate.Data;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<TodoTask> TodoTasks { get; set; }
}