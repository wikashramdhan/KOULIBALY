using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KOULIBALY.Models.Objects
{
    public class Team
    {
        

        public int Id { get; set; }
        [Required(ErrorMessage = "A team name is required")]
        public string Name { get; set; }
        [Range(1, 10, ErrorMessage = "Strength must be between 1 and 10")]
        public int Strength { get; set; }

        
        public virtual List<Game> HomeGames { get; set; }
        public virtual List<Game> AwayGames { get; set; }
    }
}
