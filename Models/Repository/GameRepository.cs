using KOULIBALY.Models.InterfaceRepository;
using KOULIBALY.Models.Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KOULIBALY.Models.Repository
{
    public class GameRepository : IGameRepository
    {
        private readonly AppDbContext _context;

        public GameRepository(AppDbContext context)
        {
            _context = context;
        }

        public Game Add(Game game)
        {
            _context.Games.Add(game);
            _context.SaveChanges();
            return game;
        }

        public Game Get(int id)
        {
            Game game = _context.Games.Include(team => team.HomeTeam)
                                      .Include(team => team.AwayTeam)
                                      .ToList().FirstOrDefault(p => p.Id == id);
            return game;
        }

        public Game Update(Game gameChanges)
        {
            var team = _context.Games.Attach(gameChanges);
            team.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return gameChanges;
        }
    }
}
