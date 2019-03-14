using System.Collections.Generic;

namespace BoardgameData.Models
{
    public interface IPlayed
    {
        IEnumerable<Played> GetAll();
        Played GetById(int id);
        void Add(Played boardgame);
        void Delete(Played boardgame);
        void Update(Played boardgame);
    }
}
