﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebAPI.Data
{
    public class WebAPIContext : DbContext
    {
        public WebAPIContext (DbContextOptions<WebAPIContext> options)
            : base(options)
        {
        }

        public DbSet<WebApplication2.Models.User>? User { get; set; }

        public DbSet<WebApplication2.Models.Contact> Contact { get; set; }

        public DbSet<WebApplication2.Models.Message> Message { get; set; }
    }
}
