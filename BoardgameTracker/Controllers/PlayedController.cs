using BoardgameData.Models;
using BoardgameTracker.Models.Played;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using BoardgameData;
using System.Linq;

namespace BoardgameTracker.Controllers
{
    public class PlayedController : Controller
    {
        private readonly IPlayed _assets;
        private readonly IBoardgame _assetBoardgame;
        private readonly IHostingEnvironment _env;

        public PlayedController(IPlayed assets, IHostingEnvironment env, IBoardgame assetBoardgame)
        {
            _assets = assets;
            _assetBoardgame = assetBoardgame;
            _env = env;
        }

        public IActionResult Index()
        {
            var model = new AssetPlayedIndex
            {
                Played = _assets.GetAll()
            };

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var played = _assets.GetById(id);

            var model = new AssetPlayedDetail()
            {
                Id = played.Id,
                Date = played.Date,
                Description = played.Description,
                Boardgame = played.Boardgame,
                Images = played.Images,
                Players = played.Players
            };

            return View(model);
        }

        public IActionResult Create()
        {
            var boardgames = _assets.GetAllBoardgames();
            var players = _assets.GetAllPlayers();

            var model = new AssetCreateIndex()
            {
                Boardgames = boardgames,
                Players = players,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AssetCreateIndex assetCreate)
        {
            if (ModelState.IsValid)
            {
                var webRoot = _env.WebRootPath;

                List<Image> images = new List<Image>();

                if (assetCreate.imageUpload != null)
                {
                    foreach (var file in assetCreate.imageUpload)
                    {
                        var filePath = Path.Combine(webRoot.ToString() + "\\images\\plays\\" + file.FileName);

                        if (file.FileName.Length > 0)
                        {
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }

                            images.Add(new Image
                            {
                                Url = "\\images\\plays\\" + file.FileName
                            });
                        }
                    }
                }

                List<PlayerPlayed> players = new List<PlayerPlayed>();

                foreach (var playerId in assetCreate.PlayerIds)
                {

                    players.Add(new PlayerPlayed
                    {
                        Player = _assets.GetPlayerById(playerId)
                    });
                }

                var played = new Played()
                {
                    Date = DateTime.Now,
                    Description = assetCreate.Descryption,
                    Boardgame = _assetBoardgame.GetById(assetCreate.BoardgameId),
                    Images = images,
                    Players = players.AsEnumerable()
                };

                _assets.Add(played);
                return RedirectToAction("Index");
            }

            return View(assetCreate);
        }

        public IActionResult Update(int id)
        {
            var boardgames = _assets.GetAllBoardgames();
            var played = _assets.GetById(id);
            var model = new AssetPlayedUpdate()
            {
                Boardgames = boardgames,
                Descryption = played.Description
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(AssetPlayedUpdate assetCreate)
        {
            if (ModelState.IsValid)
            {
                var webRoot = _env.WebRootPath;

                List<Image> images = new List<Image>();

                if (assetCreate.imageUpload != null)
                {
                    foreach (var file in assetCreate.imageUpload)
                    {
                        var filePath = Path.Combine(webRoot.ToString() + "\\images\\plays\\" + file.FileName);

                        if (file.FileName.Length > 0)
                        {
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }

                            images.Add(new Image
                            {
                                Url = "\\images\\plays\\" + file.FileName
                            });
                        }
                    }
                }

                var played = new Played()
                {
                    Id = assetCreate.Id,
                    Date = DateTime.Now,
                    Description = assetCreate.Descryption,
                    Boardgame = _assetBoardgame.GetById(assetCreate.BoardgameId)
                };

                if (images.Count > 0)
                {
                    played.Images = images;
                }

                _assets.Update(played);
                return RedirectToAction("Detail", new { Id = played.Id });
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var played = _assets.GetById(id);

            _assets.Delete(played.Images);
            _assets.Delete(played.Players);
            _assets.Delete(played);

            return RedirectToAction(nameof(Index));
        }
    }
}