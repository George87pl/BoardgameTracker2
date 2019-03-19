using BoardgameData.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BoardgameTracker.Models.Played
{
    public class AssetCreateIndex
    {
        public int Id { get; set; }

        public IEnumerable<Boardgame> Boardgames { get; set; }
        public IEnumerable<BoardgameData.Models.Player> Players { get; set; }

        public string Descryption { get; set; }
        public int BoardgameId { get; set; }

        [Required(ErrorMessage = "Player is required")]
        public int[] PlayerIds { get; set; }
       
        public IEnumerable<PlayerPlayed> PlayerPlayeds { get; set; }
        public IEnumerable<IFormFile> imageUpload { get; set; }
    }
}
