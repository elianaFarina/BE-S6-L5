using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class AlbergoDbContext : IdentityDbContext
    {
        public AlbergoDbContext(DbContextOptions<AlbergoDbContext> options)
            : base(options)
        {
        }
    }
}

