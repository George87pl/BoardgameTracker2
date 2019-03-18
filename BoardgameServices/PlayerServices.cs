using System.Collections.Generic;
using System.Linq;
using BoardgameData;
using BoardgameData.Models;
using Microsoft.EntityFrameworkCore;

namespace BoardgameServices
{
    public class PlayerServices : IPlayer
    {
        private readonly BoardgameContext _dbContext;

        public PlayerServices(BoardgameContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Player> GetAll()
        {
            return _dbContext.Players;
        }

        public Player GetById(int id)
        {
            return _dbContext.Players
                .Include(p => p.Plays)
                .ThenInclude(pl => pl.Played)
                .ThenInclude(b => b.Boardgame)
                .FirstOrDefault(b => b.Id == id);
        }

        public void Add(Player player)
        {
            _dbContext.Add(player);
            _dbContext.SaveChanges();
        }

        public void Delete(Player player)
        {
            _dbContext.Remove(player);
            _dbContext.SaveChanges();
        }

        public void Update(Player player)
        {
            _dbContext.Update(player);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Played> GetAllPlaysWhereIdPlayer(int id)
        {
            return _dbContext.Plays
                .Include(b => b.Boardgame)
                .Include(p => p.Players)
                .ThenInclude(i => i.Player)
                .Where(p => p.Players.Any(c => c.Player.Id == id));
        }
    }
}
