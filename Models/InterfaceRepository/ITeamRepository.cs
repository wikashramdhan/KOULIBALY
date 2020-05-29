using KOULIBALY.Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KOULIBALY.Models.InterfaceRepository
{
    public interface ITeamRepository
    {
        Team Get(int id);
        IEnumerable<Team> GetAllTeams();
        Team Add(Team team);
        Team Update(Team teamChanges);
        Team Delete(int id);

    }
}
