using Microsoft.EntityFrameworkCore;
using TaskManagmentSystemApiProject.Models;

namespace TaskManagmentSystemApiProject.Data
{
    public class TaskDatabaseContext : DbContext
    {
        public TaskDatabaseContext(DbContextOptions<TaskDatabaseContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Task>()
                .HasOne(p => p.AssignedTo)
                .WithMany(p => p.TasksAssigned);
            modelBuilder.Entity<Models.Task>()
                .HasOne(p => p.CreatedBy)
                .WithMany(p => p.TasksCreated);
        }
    }
}
