using JeopardyWebAPI.Data.EFCore;
using JeopardyWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JeopardyWebAPI.Test
{
    class TestHelper
    {
        private readonly JeopardyDbContext jeopardyDbContext;
        public TestHelper()
        {
            var builder = new DbContextOptionsBuilder<JeopardyDbContext>();
            builder.UseInMemoryDatabase(databaseName: "LibraryDbInMemory");

            var dbContextOptions = builder.Options;
            jeopardyDbContext = new JeopardyDbContext(dbContextOptions);
            
            // Delete existing db before creating a new one
            jeopardyDbContext.Database.EnsureDeleted();
            jeopardyDbContext.Database.EnsureCreated();
        }

        public IJeopardyRepository GetInMemoryJeopardyRepository()
        {
            return new JeopardyRepository(jeopardyDbContext);
        }

    }
}
