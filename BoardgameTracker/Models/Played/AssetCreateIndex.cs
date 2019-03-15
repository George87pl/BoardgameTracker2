using BoardgameData.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace BoardgameTracker.Models.Played
{
    public class AssetCreateIndex
    {
        public IEnumerable<Boardgame> Boardgames { get; set; }
        public IEnumerable<BoardgameData.Models.Player> Players { get; set; }

        public string Descryption { get; set; }

        public int BoardgameId { get; set; }
        public int[] PlayerIds { get; set; }

        public IEnumerable<IFormFile> imageUpload { get; set; }
    }
}
