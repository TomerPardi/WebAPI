using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RatingApp.Models;

namespace RatingApp.Data
{
    public class RatingAppContext : DbContext
    {
        public RatingAppContext (DbContextOptions<RatingAppContext> options)
            : base(options)
        {
        }

        public DbSet<RatingApp.Models.User> User { get; set; }
    }
}
