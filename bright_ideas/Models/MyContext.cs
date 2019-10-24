using Microsoft.EntityFrameworkCore;

namespace bright_ideas.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users {get;set;}
        public DbSet<Idea> Ideas {get;set;}
        public DbSet<Like> Actions {get;set;}
    }
}