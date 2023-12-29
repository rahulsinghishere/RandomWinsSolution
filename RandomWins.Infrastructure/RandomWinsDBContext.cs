using Microsoft.EntityFrameworkCore;
using RandomWins.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomWins.Infrastructure
{
    public class RandomWinsDBContext:DbContext
    {
        public RandomWinsDBContext(DbContextOptions opts):base(opts)
        {
            
        }

        public DbSet<GameSession> GameSessions { get; set; }
        public DbSet<GameSessionUser> GameSessionUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<GameSessionUser>().HasOne(i=>i.User).WithMany(i=>i.GameSessionUsers).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<GameSessionUser>().HasOne(i => i.GameSession).WithMany(i => i.GameSessionUsers).OnDelete(DeleteBehavior.Restrict);
        }
    }
}


