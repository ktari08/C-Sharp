using Microsoft.EntityFrameworkCore;

namespace activity_center.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users {get;set;}
        public DbSet<TheActivity> Activities {get;set;}
        public DbSet<Rsvp> Actions {get;set;}
    }
}