using BoardgameData;
using BoardgameTracker.Models.Colection;
using BoardgameTracker.Models.Player;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using BoardgameData.Models;

namespace BoardgameTracker.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayer _assets;
        private readonly IPlayed _played;
        private readonly IHostingEnvironment _env;

        public PlayerController(IPlayer assets, IPlayed played, IHostingEnvironment env)
        {
            _assets = assets;
            _played = played;
            _env = env;
        }

        public IActionResult Index()
        {
            var model = new AssetPlayerIndex
            {
                Players = _assets.GetAll()

            };

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var player = _assets.GetById(id);
            var plays = _assets.GetAllPlaysWhereIdPlayer(id);

            var model = new AssetPlayerDetail()
            {
                Id = player.Id,
                Description = player.Description,
                Image = player.Image,
                Name = player.Name,
                Played = plays
            };

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatePlayerModel createPlayer)
        {
            if (ModelState.IsValid)
            {
                var webRoot = _env.WebRootPath;
                var filePath = Path.Combine(webRoot.ToString() + "\\images\\players\\" + createPlayer.imageUpload.FileName);

                if (createPlayer.imageUpload.FileName.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        createPlayer.imageUpload.CopyTo(stream);
                    }
                }

                var player = new Player
                {
                    Name = createPlayer.Name,
                    Description = createPlayer.Description,
                    Image = "\\images\\players\\" + createPlayer.imageUpload.FileName
                };

                _assets.Add(player);
                return RedirectToAction("Index");
            }

            return View(createPlayer);
        }

        public IActionResult Update(int id)
        {
            var player = _assets.GetById(id);

            var model = new CreatePlayerModel()
            {
                Id = player.Id,
                Name = player.Name,
                Description = player.Description,
                Image = player.Image
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(UpdatePlayerModel playerModel)
        {
            if (ModelState.IsValid)
            {
                var player = new Player
                {
                    Id = playerModel.Id,
                    Name = playerModel.Name,
                    Image = playerModel.Image,
                    Description = playerModel.Description,
                };

                if (playerModel.imageUpload != null)
                {
                    if (playerModel.imageUpload.FileName.Length > 0)
                    {
                        var webRoot = _env.WebRootPath;
                        var filePath = Path.Combine(webRoot.ToString() + player.Image);

                        if (player.Image != null)
                        {
                            webRoot = _env.WebRootPath;
                            System.IO.File.Delete(filePath);
                        }

                        filePath = Path.Combine(webRoot.ToString() + "\\images\\players\\" +
                                                playerModel.imageUpload.FileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            playerModel.imageUpload.CopyTo(stream);
                        }
                        player.Image = "\\images\\players\\" + playerModel.imageUpload.FileName;
                    }
                }

                _assets.Update(player);

                return RedirectToAction("Detail", new { id = playerModel.Id });
            }

            return View(playerModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, string Image)
        {
            var plays = _played.GetAll();
            var player = _assets.GetById(id);

            foreach (var playItem in plays)
            {
                foreach (var playerItem in playItem.Players)
                {
                    if (playerItem.Player.Id == id)
                    {
                        ModelState.AddModelError("Id", "Cant delete");
                        return RedirectToAction(nameof(Detail), new { player.Id });
                    }
                }
            }

            _assets.Delete(player);

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