using System.Collections.Generic;
using System.Linq;
using BoardgameData;
using BoardgameData.Models;
using Microsoft.EntityFrameworkCore;

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

        public int isPlayed(int id)
        {
            return _dbContext.Plays.Count(p => p.Boardgame.Id == id);
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

        public IEnumerable<Played> GetAllPlaysWhereIdBoardgame(int id)
        {
            return _dbContext.Plays
                .Include(p => p.Players)
                .ThenInclude(pl => pl.Player)
                .Include(b => b.Boardgame)
                .Where(bg => bg.Boardgame.Id == id);
        }
    }
}
