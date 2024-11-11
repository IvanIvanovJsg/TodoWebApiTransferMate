using Microsoft.AspNetCore.Mvc;

namespace TodoWebApiTransferMate.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    public TaskController()
    {
    }

    public OkObjectResult Get()
    {
        return Ok("Hello!");
    }

}