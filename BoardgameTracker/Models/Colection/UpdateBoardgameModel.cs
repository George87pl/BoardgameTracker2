using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BoardgameTracker.Models.Colection
{
    public class UpdateBoardgameModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public int Rating { get; set; }

        public IFormFile imageUpload { get; set; }
    }
}
