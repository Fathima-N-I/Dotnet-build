using Microsoft.AspNetCore.Mvc;
using TaskManagement.Models;
using TaskManagement.Services;

namespace TaskManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    private readonly TaskService _service;

    public TasksController(TaskService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetTasks()
    {
        return Ok(_service.GetTasks());
    }
    [HttpGet("{id}")]
    public IActionResult GetTaskById(int id)
    {
        TaskItem? res = _service.GetTaskById(id);
        if(res == null)
        {
            return NotFound();
        }
        return Ok(res);
    }

    [HttpPost]
    public IActionResult AddTask(TaskItem task)
    {
        _service.AddTask(task);
        return Ok(task);
    }
}