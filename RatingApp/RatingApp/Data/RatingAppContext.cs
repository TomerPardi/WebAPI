using Microsoft.EntityFrameworkCore;

namespace RatingApp.Data
{
    public class RatingAppContext : DbContext
    {
        public RatingAppContext(DbContextOptions<RatingAppContext> options)
            : base(options)
        {
        }

        public DbSet<RatingApp.Models.User> User { get; set; }
    }
}
