using System.Collections.Generic;
using System.IO;
using System.Linq;
using BoardgameData;
using BoardgameData.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace BoardgameServices
{
    public class PlayedServices : IPlayed
    {
        private readonly BoardgameContext _dbContext;
        private readonly IHostingEnvironment _env;

        public PlayedServices(BoardgameContext dbContext, IHostingEnvironment env)
        {
            _dbContext = dbContext;
            _env = env;
        }

        public IEnumerable<Boardgame> GetAllBoardgames()
        {
            return _dbContext.Boardgames;
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            return _dbContext.Players;
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
            return _dbContext.Plays
                .Include(b => b.Boardgame)
                .Include(i => i.Images)
                .Include(pp => pp.Players)
                .ThenInclude(p => p.Player)
                .FirstOrDefault(b => b.Id == id);
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

        public void Delete(IEnumerable<Image> images)
        {
            foreach (var image in images)
            {

                var webRoot = _env.WebRootPath;
                var filePath = Path.Combine(webRoot.ToString() + image.Url);
                File.Delete(filePath);

                _dbContext.Remove(image);
            }
            _dbContext.SaveChanges();
        }

        public void Delete(IEnumerable<PlayerPlayed> playerPlays)
        {
            foreach (var playerPlay in playerPlays)
            {
                _dbContext.Remove(playerPlay);
            }
            _dbContext.SaveChanges();
        }

        public void Update(Played played)
        {
            _dbContext.Update(played);
            _dbContext.SaveChanges();
        }
    }
}
