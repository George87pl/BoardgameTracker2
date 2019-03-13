using System.Collections.Generic;
using BoardgameData.Models;

namespace BoardgameTracker.Models.Colection
{
    public class AssetIndexListingModel
    {
        public IEnumerable<Boardgame> Boardgames { get; set; }
    }
}
