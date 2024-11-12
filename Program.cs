using Microsoft.EntityFrameworkCore;
using TodoWebApiTransferMate.Data;

var builder = WebApplication.CreateBuilder(args);

// TODO: Add logging middleware
// TODO: Make swagger be on the main endpoint
// TODO: See what to do with the connection string and the credentials in docker-compose
builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseNpgsql("Host=localhost;Port=5432;Database=TodoDb;Username=transfermate;Password=transfermatepassword"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();