using BoardgameData.Models;
using System.Collections.Generic;

namespace BoardgameData
{
    public interface IPlayer
    {
        IEnumerable<Player> GetAll();
        Player GetById(int id);
        IEnumerable<Played> GetAllPlaysWhereIdPlayer(int id);
        void Add(Player player);
        void Delete(Player player);
        void Update(Player player);
    }
}
