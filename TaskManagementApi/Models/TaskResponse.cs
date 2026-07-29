namespace TaskManagement.Models;

public class TaskResponse
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public bool Completed { get; set; }
    public string? CategoryName { get; set;}
}