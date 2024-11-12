using Microsoft.EntityFrameworkCore;
using NSubstitute;
using TodoWebApiTransferMate.Data;
using TodoWebApiTransferMate.Models;
using TodoWebApiTransferMate.Models.Entities;
using TodoWebApiTransferMate.Services;
using Xunit;

namespace TodoWebApiTransferMate.Tests;

public class TodoTaskServiceTest
{
    private readonly TodoDbContext _context;
    private readonly ICurrentTimeProvider _mockTimeProvider;
    private readonly TodoTaskService _todoTaskService;

    public TodoTaskServiceTest()
    {
        var options = new DbContextOptionsBuilder<TodoDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new TodoDbContext(options);
        _mockTimeProvider = Substitute.For<ICurrentTimeProvider>();
        _todoTaskService = new TodoTaskService(_context, _mockTimeProvider);
    }

    [Fact]
    public async Task GetAllPendingTasksAsync_ShouldReturnOnlyPendingTasks()
    {
        // Arrange
        var currentTime = new DateTime(2024, 11, 12);
        _mockTimeProvider.GetCurrentTime().Returns(currentTime);

        var tasks = new List<TodoTask>
        {
            new TodoTask { Title = "Task 1", DueDate = currentTime.AddDays(1), State = TodoTaskState.InProgress },
            new TodoTask { Title = "Task 2", DueDate = currentTime.AddDays(-1), State = TodoTaskState.InProgress },
            new TodoTask { Title = "Task 3", DueDate = null, State = TodoTaskState.InProgress },
            new TodoTask { Title = "Task 4", DueDate = currentTime.AddDays(1), State = TodoTaskState.Completed }
        };
        await _context.TodoTasks.AddRangeAsync(tasks);
        await _context.SaveChangesAsync();

        // Act
        var result = await _todoTaskService.GetAllPendingTasksAsync();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Contains(result, t => t.Title == "Task 1");
        Assert.Contains(result, t => t.Title == "Task 3");
    }
    
        [Fact]
        public async Task GetAllOverdueTasksAsync_ShouldReturnOnlyOverdueTasks()
        {
            // Arrange
            var currentTime = new DateTime(2024, 11, 12);
            _mockTimeProvider.GetCurrentTime().Returns(currentTime);
    
            var tasks = new List<TodoTask>
            {
                new TodoTask { Title = "Task 1", DueDate = currentTime.AddDays(-1), State = TodoTaskState.InProgress },
                new TodoTask { Title = "Task 2", DueDate = currentTime.AddDays(1), State = TodoTaskState.InProgress },
                new TodoTask { Title = "Task 3", DueDate = null, State = TodoTaskState.InProgress },
                new TodoTask { Title = "Task 4", DueDate = currentTime.AddDays(-2), State = TodoTaskState.Completed }
            };
            await _context.TodoTasks.AddRangeAsync(tasks);
            await _context.SaveChangesAsync();
    
            // Act
            var result = await _todoTaskService.GetAllOverdueTasksAsync();
    
            // Assert
            Assert.Single(result);
            Assert.Contains(result, t => t.Title == "Task 1");
        }
}