using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BoardgameTracker.Models.Player
{
    public class CreatePlayerModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        [Required]
        public IFormFile imageUpload { get; set; }
    }
}
