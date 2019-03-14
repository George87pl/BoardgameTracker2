using System.Collections.Generic;

namespace BoardgameData.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public virtual IEnumerable<PlayerPlayed> Plays { get; set; }

    }
}
