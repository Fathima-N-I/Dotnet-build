using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models;

public class UpdateTaskRequest
{
    [StringLength(100)]
    [Required]
    public string Title { get; set; } = string.Empty;
    public bool Completed { get; set; }
}