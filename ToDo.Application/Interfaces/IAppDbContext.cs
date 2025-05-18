using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ToDo.Domain.Models;

namespace ToDo.Application.Interfaces
{
    public interface IAppDbContext
    {
        DatabaseFacade Database { get; }
        DbSet<TaskItem> TaskItems { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
