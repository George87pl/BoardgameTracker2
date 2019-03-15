using BoardgameData.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace BoardgameTracker.Models.Played
{
    public class CreatePlayedModel
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public Boardgame Boardgame { get; set; }
        public IEnumerable<Image> Images { get; set; }
        public IEnumerable<PlayerPlayed> Players { get; set; }

        public List<IFormFile> Files { get; set; }
    }
}
