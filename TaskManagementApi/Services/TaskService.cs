using TaskManagement.Models;
using TaskManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace TaskManagement.Services;

public class TaskService 
{
    private readonly AppDbContext _context;

    public TaskService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TaskItem>> GetTasks()
    {
        return await _context.Tasks.ToListAsync();
    }

    public async Task<TaskItem?> GetTaskById(int id)
    {
        return await _context.Tasks.FirstOrDefaultAsync(e=>e.Id == id);
    }

    public async Task<TaskItem> AddTask(CreateTaskRequest request)
    {
        TaskItem newTask = new();
        newTask.Title = request.Title;
        newTask.Completed = request.Completed;
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
