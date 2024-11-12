using Microsoft.EntityFrameworkCore;
using TodoWebApiTransferMate.Data;
using TodoWebApiTransferMate.Services;

var builder = WebApplication.CreateBuilder(args);

// TODO: See what to do with the connection string and the credentials in docker-compose
// TODO: Change host name depending on who runs dotnet ef dattabase update
// TODO: Force mindate to today

builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseNpgsql("Host=db;Port=5432;Database=TodoDb;Username=transfermate;Password=transfermatepassword"));
builder.Services.AddScoped<ITodoTaskService, TodoTaskService>();
builder.Services.AddScoped<ICurrentTimeProvider, CurrentTimeProvider>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();