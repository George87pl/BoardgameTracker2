﻿using System.Collections.Generic;
using System.Linq;
using BoardgameData;
using BoardgameData.Models;

namespace BoardgameServices
{
    public class BGServices : IBoardgame
    {
        private readonly BoardgameContext _dbContext;

        public BGServices(BoardgameContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Boardgame> GetAll()
        {
            return _dbContext.Boardgames;
        }

        public Boardgame GetById(int id)
        {
            return GetAll().FirstOrDefault(b => b.Id == id);
        }
    }
}
