using System.Diagnostics.Eventing.Reader;
using System.Net.NetworkInformation;
using TaskManagement.Models;
namespace TaskManagement.Services;

public class TaskService 
{
    private List<TaskItem> tasks = new();

    public TaskService()
    {
        TaskItem ob1 = new TaskItem{ Id = 1, Title  = "Learn C#", Completed = true };
        TaskItem ob2 = new TaskItem{ Id = 2, Title  = "Learn C#", Completed = false };
        TaskItem ob3 = new TaskItem{ Id = 3, Title  = "Learn C#", Completed = true };   
        tasks.Add(ob1);
        tasks.Add(ob2);
        tasks.Add(ob3);
    }

    public List<TaskItem> GetTasks()
    {
        return tasks;
    }

    public TaskItem? GetTaskById(int id)
    {
        return tasks.FirstOrDefault(e=>e.Id == id);
    }

    public void AddTask(TaskItem task)
    {
        tasks.Add(task);
    }

    public bool UpdateTask(int id, TaskItem task)
    {
        TaskItem? existingTask = tasks.FirstOrDefault(e=>e.Id == id);
        if (existingTask == null)
        {
            return false;
        }
        existingTask.Title = task.Title;
        existingTask.Completed = task.Completed;
        return true;
    }

    public bool DeleteTask(int id)
    {
        TaskItem? ToDeleteTask = tasks.FirstOrDefault(e=>e.Id == id);
        if (ToDeleteTask == null)
        {
            return false;
        }
        tasks.Remove(ToDeleteTask);
        return true;
    }
}
