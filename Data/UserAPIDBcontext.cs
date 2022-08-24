using BCEPtest.Models;
using Microsoft.EntityFrameworkCore;


namespace BCEPtest.Data
{
    public class UserAPIDBcontext : DbContext
    {
        public UserAPIDBcontext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
