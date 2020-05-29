using KOULIBALY.Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KOULIBALY.Models.InterfaceRepository
{
    public interface IPouleRepository
    {
        Poule Add(Poule poule);
        Poule Get(int id);
    }
}
