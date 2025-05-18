using Microsoft.EntityFrameworkCore;
using ToDo.Application.Interfaces;
using ToDo.Domain.Models;
using ToDo.Persistance.EntityTypeConfigurations;

namespace ToDo.Persistance
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public DbSet<TaskItem> TaskItems { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TaskItemConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
