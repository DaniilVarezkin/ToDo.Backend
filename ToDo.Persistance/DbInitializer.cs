using Microsoft.EntityFrameworkCore;

namespace ToDo.Persistance
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext dbContext)
        {
            //dbContext.Database.EnsureCreated();
            dbContext.Database.Migrate();
        }
    }
}
