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
        private readonly IPlayer _assetPlayer;
        private readonly IBoardgame _assetBoardgame;
        private readonly IHostingEnvironment _env;

        public PlayedController(IPlayed assets, IPlayer assetPlayer, IHostingEnvironment env, IBoardgame assetBoardgame)
        {
            _assets = assets;
            _assetPlayer = assetPlayer;
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
                Descryption = ""
            };

            return View(model);
        }

        public IActionResult CreateScores(AssetCreateIndex assetCreate)
        {
            List<PlayerPlayed> players = new List<PlayerPlayed>();

            foreach (var playerId in assetCreate.PlayerIds)
            {

                players.Add(new PlayerPlayed
                {
                    Player = _assetPlayer.GetById(playerId)
                });
            }

            assetCreate.PlayerPlayeds = players.AsEnumerable();
            assetCreate.Boardgame = _assetBoardgame.GetById(assetCreate.BoardgameId);

            return View(assetCreate);
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

                var played = new Played()
                {
                    Date = DateTime.Now,
                    Description = assetCreate.Descryption,
                    Boardgame = _assetBoardgame.GetById(assetCreate.BoardgameId),
                    Images = images,
                    Players = assetCreate.PlayerPlayeds
                };

                _assets.Add(played);
                return RedirectToAction("Index");
            }

            return View(assetCreate);
        }

        //public IActionResult Update(int id)
        //{
        //    var boardgame = _assets.GetById(id);

        //    var model = new CreateBoardgameModel()
        //    {
        //        Id = boardgame.Id,
        //        Name = boardgame.Name,
        //        Description = boardgame.Description,
        //        Rating = boardgame.Rating,
        //        Image = boardgame.Image
        //    };

        //    return View(model);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Update(UpdateBoardgameModel boardgameModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var boardgame = new Boardgame
        //        {
        //            Id = boardgameModel.Id,
        //            Name = boardgameModel.Name,
        //            Image = boardgameModel.Image,
        //            Description = boardgameModel.Description,
        //            Rating = boardgameModel.Rating
        //        };

        //        if (boardgameModel.imageUpload != null)
        //        {
        //            if (boardgameModel.imageUpload.FileName.Length > 0)
        //            {
        //                var webRoot = _env.WebRootPath;
        //                var filePath = Path.Combine(webRoot.ToString() + boardgame.Image);

        //                if (boardgame.Image != null)
        //                {
        //                    webRoot = _env.WebRootPath;
        //                    System.IO.File.Delete(filePath);
        //                }

        //                filePath = Path.Combine(webRoot.ToString() + "\\images\\games\\" +
        //                                        boardgameModel.imageUpload.FileName);

        //                using (var stream = new FileStream(filePath, FileMode.Create))
        //                {
        //                    boardgameModel.imageUpload.CopyTo(stream);
        //                }
        //                boardgame.Image = "\\images\\games\\" + boardgameModel.imageUpload.FileName;
        //            }
        //        }

        //        _assets.Update(boardgame);

        //        return RedirectToAction("Detail", new { id = boardgameModel.Id });
        //    }

        //    return View(boardgameModel);
        //}

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