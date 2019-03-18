using System.Collections.Generic;

namespace BoardgameData.Models
{
    public interface IPlayed
    {
        IEnumerable<Boardgame> GetAllBoardgames();
        IEnumerable<Player> GetAllPlayers();
        IEnumerable<Played> GetAll();
        Played GetById(int id);
        void Add(Played boardgame);
        void Delete(Played boardgame);
        void Delete(IEnumerable<Image> images);
        void Delete(IEnumerable<PlayerPlayed> playerPlays);
        void Update(Played boardgame);
    }
}
