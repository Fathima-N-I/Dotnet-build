namespace TaskManagement.Models;

public class CreateTaskRequest
{
    public string Title {get; set;} = string.Empty;
    public bool Completed {get; set;}
}