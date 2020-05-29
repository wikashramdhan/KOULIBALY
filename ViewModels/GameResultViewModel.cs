using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KOULIBALY.ViewModels
{
    public class GameResultViewModel
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int Points { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference { get; set; }
    }

}
