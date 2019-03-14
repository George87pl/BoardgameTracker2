using BoardgameData.Models;
using System.Collections.Generic;

namespace BoardgameData
{
    public interface IPlayer
    {
        IEnumerable<Player> GetAll();
        Player GetById(int id);
        void Add(Player boardgame);
        void Delete(Player boardgame);
        void Update(Player boardgame);
    }
}
