using System.Collections.Generic;
using BoardgameData.Models;

namespace BoardgameTracker.Models.Played
{
    public class AssetPlayedIndex
    {
        public int Id { get; set; }
        public IEnumerable<BoardgameData.Models.Played> Played { get; set; }
    }
}
