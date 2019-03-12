using System;
using System.Collections.Generic;
using System.Text;

namespace BoardgameData.Models
{
    public class Played
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public IEnumerable<Image> Images { get; set; }
        public IEnumerable<Player> Players { get; set; }
    }
}
