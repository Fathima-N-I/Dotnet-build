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
    public async Task<IActionResult> GetTasks()
    {
        return Ok(await _service.GetTasks());
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskById(int id)
    {
        TaskItem? res = await _service.GetTaskById(id);
        if(res == null)
        {
            return NotFound();
        }
        return Ok(res);
    }

    [HttpPost]
    public async Task<IActionResult> AddTask(CreateTaskRequest request)
    {
        TaskItem createdTask = await _service.AddTask(request);
        return CreatedAtAction(
            nameof(GetTaskById),
            new { id = createdTask.Id},
            createdTask
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id,UpdateTaskRequest updateTaskRequest)
    {
        bool updated = await _service.UpdateTask(id, updateTaskRequest);
        if (!updated)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        bool deleted = await _service.DeleteTask(id);
        if(!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}