using KOULIBALY.Models.InterfaceRepository;
using KOULIBALY.Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KOULIBALY.Models
{
    public class TeamRepository : ITeamRepository
    {
        private readonly AppDbContext _context;

        public TeamRepository(AppDbContext context)
        {
            _context = context;
        }


        public Team Add(Team team)
        {
            _context.Teams.Add(team);
            _context.SaveChanges();
            return team;
        }

        public Team Delete(int id)
        {
            Team team = _context.Teams.Find(id);
            if(team != null)
            {
                _context.Teams.Remove(team);
                _context.SaveChanges();
            }
            return team;
        }

        public IEnumerable<Team> GetAllTeams()
        {
            return _context.Teams;
        }

        public Team Get(int id)
        {
            return _context.Teams.Find(id);
        }

        public Team Update(Team teamChanges)
        {
            var team = _context.Teams.Attach(teamChanges);
            team.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return teamChanges;

        }
    }
}
