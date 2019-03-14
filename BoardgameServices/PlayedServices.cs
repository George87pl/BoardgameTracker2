using System.Collections.Generic;
using System.Linq;
using BoardgameData;
using BoardgameData.Models;
using Microsoft.EntityFrameworkCore;

namespace BoardgameServices
{
    public class PlayedServices : IPlayed
    {
        private readonly BoardgameContext _dbContext;

        public PlayedServices(BoardgameContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Played> GetAll()
        {
            return _dbContext.Plays
                .Include(b => b.Boardgame)
                .Include(pp => pp.Players)
                .ThenInclude(p => p.Player);

        }

        public Played GetById(int id)
        {
            return GetAll().FirstOrDefault(b => b.Id == id);
        }

        public void Add(Played played)
        {
            _dbContext.Add(played);
            _dbContext.SaveChanges();
        }

        public void Delete(Played played)
        {
            _dbContext.Remove(played);
            _dbContext.SaveChanges();
        }

        public void Update(Played played)
        {
            _dbContext.Update(played);
            _dbContext.SaveChanges();
        }
    }
}
