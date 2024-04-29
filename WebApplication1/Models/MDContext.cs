using Microsoft.EntityFrameworkCore;
using System.Net;

namespace WebApplication1.Models
{
    public class MDContext:DbContext
    {
        public MDContext(DbContextOptions<MDContext> options) : base(options)
        {

        }

        public DbSet<Faculty> Faculty { get; set;}
        public DbSet<Student> Students { get; set;}
    }
}
