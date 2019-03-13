using System;
using System.Collections.Generic;

namespace BoardgameData.Models
{
    public class Played
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        Boardgame Boardgame { get; set; }
        public IEnumerable<Image> Images { get; set; }
        public IEnumerable<PlayerPlayed> Players { get; set; }
    }
}
