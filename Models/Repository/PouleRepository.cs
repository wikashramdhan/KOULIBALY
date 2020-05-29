using KOULIBALY.Models.InterfaceRepository;
using KOULIBALY.Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KOULIBALY.Models.Repository
{
    public class PouleRepository : IPouleRepository
    {
        private readonly AppDbContext _context;

        public PouleRepository(AppDbContext context)
        {
            _context = context;
        }

        public Poule Add(Poule poule)
        {
            _context.Poules.Add(poule);
            _context.SaveChanges();

            return poule;
        }

        public Poule Get(int id)
        {
            Poule poule = _context.Poules.Include(game => game.Games)
                                         .ThenInclude(team => team.HomeTeam)
                                         .Include(game => game.Games)
                                         .ThenInclude(team => team.AwayTeam)
                                         .ToList().FirstOrDefault(p => p.Id == id);
            return poule;
        }
    }
}
