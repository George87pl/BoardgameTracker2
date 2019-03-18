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
            var played = _assets.isPlayed(id);

            var model = new AssetDetailModel()
            {
                Id = boardgame.Id,
                Name = boardgame.Name,
                Description = boardgame.Description,
                Image = boardgame.Image,
                Rating = boardgame.Rating,
                IsPlayed = played
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

        public IActionResult Update(int id)
        {
            var boardgame = _assets.GetById(id);

            var model = new CreateBoardgameModel()
            {
                Id = boardgame.Id,
                Name = boardgame.Name,
                Description = boardgame.Description,
                Rating = boardgame.Rating,
                Image = boardgame.Image
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(UpdateBoardgameModel boardgameModel)
        {
            if (ModelState.IsValid)
            {
                var boardgame = new Boardgame
                {
                    Id = boardgameModel.Id,
                    Name = boardgameModel.Name,
                    Image = boardgameModel.Image,
                    Description = boardgameModel.Description,
                    Rating = boardgameModel.Rating
                };

                if (boardgameModel.imageUpload != null)
                {
                    if (boardgameModel.imageUpload.FileName.Length > 0)
                    {
                        var webRoot = _env.WebRootPath;
                        var filePath = Path.Combine(webRoot.ToString() + boardgame.Image);

                        if (boardgame.Image != null)
                        {
                            webRoot = _env.WebRootPath;
                            System.IO.File.Delete(filePath);
                        }

                        filePath = Path.Combine(webRoot.ToString() + "\\images\\games\\" +
                                                boardgameModel.imageUpload.FileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            boardgameModel.imageUpload.CopyTo(stream);
                        }
                        boardgame.Image = "\\images\\games\\" + boardgameModel.imageUpload.FileName;
                    }
                }

                _assets.Update(boardgame);

                return RedirectToAction("Detail", new { id = boardgameModel.Id });
            }

            return View(boardgameModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, string Image)
        {
            var boardgame = _assets.GetById(id);
            _assets.Delete(boardgame);

            //Usuwanie pliku
            if (Image != null)
            {
                var webRoot = _env.WebRootPath;
                var filePath = Path.Combine(webRoot.ToString() + Image);
                System.IO.File.Delete(filePath);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}