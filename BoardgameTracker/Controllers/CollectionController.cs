using BoardgameData;
using BoardgameTracker.Models.Colection;
using Microsoft.AspNetCore.Mvc;

namespace BoardgameTracker.Controllers
{
    public class CollectionController : Controller
    {
        private readonly IBoardgame _assets;

        public CollectionController(IBoardgame assets)
        {
            _assets = assets;
        }

        public IActionResult Index()
        {
            var model = new AssetIndexListingModel
            {
                Boardgames = _assets.GetAll()

            };

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var boardgame = _assets.GetById(id);

            var model = new AssetDetailModel()
            {
                Id = boardgame.Id,
                Name = boardgame.Name,
                Description = boardgame.Description,
                Image = boardgame.Image,
                Rating = boardgame.Rating
            };

            return View(model);
        }
    }
}