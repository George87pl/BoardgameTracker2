using System;
using System.Collections.Generic;
using System.Text;

namespace BoardgameData.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public IEnumerable<Boardgame> Boardgames { get; set; }

    }
}
