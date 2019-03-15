using BoardgameData.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BoardgameTracker.Models.Played
{
    public class CreatePlayedAllModel
    {
        public IEnumerable<Boardgame> Boardgames { get; set; }
        public IEnumerable<BoardgameData.Models.Player> Players { get; set; }

        public CreatePlayedModel PlayedModel { get; set; }
    }
}
