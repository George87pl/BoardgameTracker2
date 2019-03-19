using BoardgameData.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace BoardgameTracker.Models.Played
{
    public class AssetPlayedUpdate
    {
        public int Id { get; set; }

        public IEnumerable<Boardgame> Boardgames { get; set; }
        public string Descryption { get; set; }
        public int BoardgameId { get; set; }
       
        public IEnumerable<PlayerPlayed> PlayerPlayeds { get; set; }
        public IEnumerable<IFormFile> imageUpload { get; set; }
    }
}
