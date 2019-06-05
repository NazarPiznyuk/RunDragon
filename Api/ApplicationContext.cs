using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public class ApplicationContext : DbContext 
    {
        public DbSet<Leaderboard> Leaderboard { get; set; }
        public DbSet<User> Users { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {
            this.Database.EnsureCreated();
        }
    }
}
