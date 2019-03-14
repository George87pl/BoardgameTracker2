using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BoardgameTracker.Models.Player
{
    public class UpdatePlayerModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public IFormFile imageUpload { get; set; }
    }
}
