using System.IO;
using System.Runtime.CompilerServices;
using BoardgameData;
using BoardgameData.Models;
using BoardgameTracker.Models.Colection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BoardgameTracker.Controllers
{
    public class CollectionController : Controller
    {
        private readonly IBoardgame _assets;
        private readonly IHostingEnvironment _env;

        public CollectionController(IBoardgame assets, IHostingEnvironment env)
        {
            _assets = assets;
            _env = env;
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

        public IActionResult Create()
        {       
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateBoardgameModel createBoardgame)
        {
            if (ModelState.IsValid)
            {
                var webRoot = _env.WebRootPath;
                var filePath = Path.Combine(webRoot.ToString() + "\\images\\games\\" + createBoardgame.imageUpload.FileName);

                if (createBoardgame.imageUpload.FileName.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        createBoardgame.imageUpload.CopyTo(stream);
                    }
                }

                var boardgame = new Boardgame
                {
                    Name = createBoardgame.Name,
                    Description = createBoardgame.Description,
                    Rating = createBoardgame.Rating,
                    Image = "\\images\\games\\" + createBoardgame.imageUpload.FileName
                };

                _assets.Add(boardgame);
                return RedirectToAction("Index");
            }

            return View(createBoardgame);
        }
    }
}