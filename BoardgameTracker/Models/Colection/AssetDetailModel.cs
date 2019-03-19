using System.Collections.Generic;

namespace BoardgameTracker.Models.Colection
{
    public class AssetDetailModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Rating { get; set; }
        public int IsPlayed { get; set; }

        public IEnumerable<BoardgameData.Models.Played> Played { get; set; }
    }
}
