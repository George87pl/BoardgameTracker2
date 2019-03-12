using System;
using BoardgameData.Models;
using Microsoft.EntityFrameworkCore;

namespace BoaedgameData
{
    public class BoardgameContext : DbContext
    {
        public BoardgameContext(DbContextOptions options) : base(options) { }

        public DbSet<Boardgame> Boardgames { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Played> Plays { get; set; }
    }
}
