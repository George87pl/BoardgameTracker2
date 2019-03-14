using System.Collections.Generic;
using System.Linq;
using BoardgameData;
using BoardgameData.Models;

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
            return GetAll().FirstOrDefault(b => b.Id == id);
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
    }
}
