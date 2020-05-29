using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KOULIBALY.ViewModels
{
    public class EditTeamViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Team name is required")]
        public string Name { get; set; }
        [Range(1, 10, ErrorMessage = "Strength must be between 1 and 10")]
        public int Strength { get; set; }
    }
}
