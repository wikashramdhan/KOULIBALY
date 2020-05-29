using KOULIBALY.Models.InterfaceRepository;
using KOULIBALY.Models.Objects;
using KOULIBALY.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace KOULIBALY.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamRepository _teamRepository;

        public TeamController(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var teams = _teamRepository.GetAllTeams().OrderByDescending(t => t.Strength);
            return View(teams);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var team = _teamRepository.Get(id);

            if(team == null)
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            var model = new EditTeamViewModel
            {
                Id = id,
                Name = team.Name,
                Strength = team.Strength
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditTeamViewModel model)
        {
            if(ModelState.IsValid)
            {
                Team team = _teamRepository.Get(model.Id);

                if (team == null)
                {
                    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                }
                else
                {
                    team.Name = model.Name;
                    team.Strength = model.Strength;
                    _teamRepository.Update(team);
                    return RedirectToAction("Index", new { Id = team.Id });
                }
            }
            

            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Team team)
        {
            if(ModelState.IsValid)
            {
                Team newTeam = _teamRepository.Add(team);
                return RedirectToAction("Index");
            }
            return View(team);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var team = _teamRepository.Get(id);


            if (team == null)
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
            else
            {
                
                _teamRepository.Delete(id);
                return RedirectToAction("Index");
            }
        }
    }
}
