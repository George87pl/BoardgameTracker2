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
    }
}