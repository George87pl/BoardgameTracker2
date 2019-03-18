using System.Collections.Generic;
using System.Linq;
using BoardgameData;
using BoardgameData.Models;

namespace BoardgameServices
{
    public class BGServices : IBoardgame
    {
        private readonly BoardgameContext _dbContext;

        public BGServices(BoardgameContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Boardgame> GetAll()
        {
            return _dbContext.Boardgames;
        }

        public Boardgame GetById(int id)
        {
            return GetAll().FirstOrDefault(b => b.Id == id);
        }

        public Played isPlayed(int id)
        {
            return _dbContext.Plays.FirstOrDefault(p => p.Boardgame.Id == id);
        }

        public void Add(Boardgame boardgame)
        {
            _dbContext.Add(boardgame);
            _dbContext.SaveChanges();
        }

        public void Delete(Boardgame boardgame)
        {
            _dbContext.Remove(boardgame);
            _dbContext.SaveChanges();
        }

        public void Update(Boardgame boardgame)
        {
            _dbContext.Update(boardgame);
            _dbContext.SaveChanges();
        }
    }
}
