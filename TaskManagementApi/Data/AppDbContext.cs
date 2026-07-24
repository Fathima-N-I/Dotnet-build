using Microsoft.EntityFrameworkCore;
using TaskManagement.Models;

namespace TaskManagement.Data;

public class AppDbContext:DbContext
{
    public DbSet<TaskItem> Tasks { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
            
        }
    public DbSet<Category> Categories { get; set; }
}