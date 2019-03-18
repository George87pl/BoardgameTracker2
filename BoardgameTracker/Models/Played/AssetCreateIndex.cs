using BoardgameData.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices.ComTypes;

namespace BoardgameTracker.Models.Played
{
    public class AssetCreateIndex
    {
        public IEnumerable<Boardgame> Boardgames { get; set; }
        public IEnumerable<BoardgameData.Models.Player> Players { get; set; }

        public string Descryption { get; set; }

        public int BoardgameId { get; set; }

        public Boardgame Boardgame { get; set; }

        [Required(ErrorMessage = "Player is required")]
        public int[] PlayerIds { get; set; }

        public IEnumerable<PlayerPlayed> PlayerPlayeds { get; set; }

        public IEnumerable<IFormFile> imageUpload { get; set; }

        public int Scores { get; set; }
        public bool isWinner { get; set; }
    }
}
