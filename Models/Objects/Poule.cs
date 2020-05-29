using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KOULIBALY.Models.Objects
{
    public class Poule
    {
        public Poule()
        {
            Games = new List<Game>();
        }
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsPlayed { get; set; }

        public List<Game> Games { get; set; }
    }
}
