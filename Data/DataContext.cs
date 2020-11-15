using Microsoft.EntityFrameworkCore;
using Task5.Models;

namespace Task5.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
 
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Match> Matches { get; set; }
        
        public DbSet<PlayerGoalMatch> PlayerGoalMatches { get; set; }
        
        
        
    }
}