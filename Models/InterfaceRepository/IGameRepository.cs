using KOULIBALY.Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KOULIBALY.Models.InterfaceRepository
{
    public interface IGameRepository
    {
        Game Add(Game game);
        Game Get(int id);
        Game Update(Game gameChanges);
    }
}
