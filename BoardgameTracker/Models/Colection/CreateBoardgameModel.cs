using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BoardgameTracker.Models.Colection
{
    public class CreateBoardgameModel
    {
        [Required]
        public string Name { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public int Rating { get; set; }

        [Required]
        public IFormFile imageUpload { get; set; }
    }
}
