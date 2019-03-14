using BoardgameData.Models;
using System;
using System.Collections.Generic;

namespace BoardgameTracker.Models.Played
{
    public class AssetPlayedDetail
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public Boardgame Boardgame { get; set; }
        public IEnumerable<Image> Images { get; set; }
        public IEnumerable<PlayerPlayed> Players { get; set; }
    }
}
