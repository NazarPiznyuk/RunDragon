using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonRun
{
   public class ApllicationContext : DbContext

    {
        private string _databasePath;
        public DbSet<DbModel> DbModels { get; set; }
        public ApllicationContext(string databasePath)
        {
            _databasePath = databasePath;
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename ={_databasePath}");
        }
    }
    public interface IPath
    {
        string GetDatabasePath(string filename);
    }
}
