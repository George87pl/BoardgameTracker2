using BoardgameData.Models;
using System.Collections.Generic;

namespace BoardgameTracker.Models.Player
{
    public class AssetPlayerDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public IEnumerable<BoardgameData.Models.Played> Played { get; set; } 
    }
}
