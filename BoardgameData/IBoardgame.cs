using BoardgameData.Models;
using System.Collections.Generic;

namespace BoardgameData
{
    public interface IBoardgame
    {
        IEnumerable<Boardgame> GetAll();
        Boardgame GetById(int id);
        int isPlayed(int id);
        void Add(Boardgame boardgame);
        void Delete(Boardgame boardgame);
        void Update(Boardgame boardgame);
    }
}
