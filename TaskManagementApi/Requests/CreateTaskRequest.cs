using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models;

public class CreateTaskRequest
{
    [Required]
    [StringLength(100)]
    public string Title {get; set;} = string.Empty;
    public bool Completed {get; set;}
}