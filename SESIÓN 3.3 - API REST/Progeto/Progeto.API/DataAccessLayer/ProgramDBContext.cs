using Microsoft.EntityFrameworkCore;
using Progeto.API.Models;

namespace Progeto.API.DataAccessLayer
{
    public class ProgramDBContext : DbContext
    {
        public DbSet<LuaProgram> Programs { get; set; }

        public ProgramDBContext(DbContextOptions<ProgramDBContext> options)
            : base(options)
        {
        }
    }
}
