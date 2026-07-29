using TaskManagement.Models;
using TaskManagement.Data;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Controllers;
using Microsoft.AspNetCore.Http.Connections;

namespace TaskManagement.Services;

public class TaskService 
{
    private readonly AppDbContext _context;

    public TaskService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TaskResponse>> GetTasks()
    {
        //return await _context.Tasks
          //  .Include(t=>t.Category)
            //.ToListAsync();
        var response = await _context.Tasks.Select(task => new TaskResponse
        {
            Id = task.Id,
            Title = task.Title,
            Completed = task.Completed,
            CategoryName = task.Category==null?"Uncategorized" : task.Category.Name
        })
        .ToListAsync();
        return response;
    }

    public async Task<TaskResponse?> GetTaskById(int id)
    {
        TaskItem? tasks = await _context.Tasks
            .Include(t=>t.Category)
            .FirstOrDefaultAsync(e=>e.Id == id);
        if(tasks == null)
        {
            return null;
        }
        TaskResponse? taskres = new();
        taskres.Id = tasks.Id;
        taskres.Title = tasks.Title;
        taskres.Completed = tasks.Completed;
        taskres.CategoryName = tasks.Category?.Name ?? "Uncategorized";
        return taskres;
    }

    public async Task<TaskItem?> AddTask(CreateTaskRequest request)
    {
        TaskItem newTask = new();
        newTask.Title = request.Title;
        newTask.Completed = request.Completed;
        newTask.CategoryId = request.CategoryId;
        if ( newTask.CategoryId != null )
        {
            bool exist = await _context.Categories.AnyAsync(e => e.Id == newTask.CategoryId);
            if(!exist)
            {
                return null;
            }
        }
        await _context.Tasks.AddAsync(newTask);
        await _context.SaveChangesAsync();
        return newTask;
    }
    public async Task<bool> UpdateTask(int id, UpdateTaskRequest updateTaskRequest)
    {
        TaskItem? existingTask = await _context.Tasks.FirstOrDefaultAsync(e => e.Id == id);
        if (existingTask == null)
        {
            return false;
        }
        existingTask.Title = updateTaskRequest.Title;
        existingTask.Completed = updateTaskRequest.Completed;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteTask(int id)
    {
        TaskItem? toDeleteTask = await _context.Tasks.FirstOrDefaultAsync(e=>e.Id == id);
        if (toDeleteTask == null)
        {
            return false;
        }
        _context.Tasks.Remove(toDeleteTask);
        await _context.SaveChangesAsync();
        return true;
    }
}
