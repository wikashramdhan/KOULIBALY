using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KOULIBALY.ViewModels;
using KOULIBALY.Models.InterfaceRepository;
using Newtonsoft.Json;
using KOULIBALY.Models.Objects;

namespace KOULIBALY.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IPouleRepository _pouleRepository;
        private readonly IGameRepository _gameRepository;


        public HomeController(ITeamRepository teamRepository, IPouleRepository pouleRepository, IGameRepository gameRepository)
        {
            _teamRepository = teamRepository;
            _pouleRepository = pouleRepository;
            _gameRepository = gameRepository;
        }

        
        public IActionResult Index()
        {
            IEnumerable<HomeTeamViewModel> teamsWithIsSelected = from t in _teamRepository.GetAllTeams().OrderByDescending(t => t.Strength)
                                                                 select new HomeTeamViewModel { Id = t.Id, Name = t.Name, IsSelected = false };
            return View(teamsWithIsSelected.ToList());
            
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult SimulatePoule(List<HomeTeamViewModel> model)
        {
            int? pouleId = null;

            if (ModelState.IsValid)
            {
                var result = model.FindAll(h => h.IsSelected == true);
                int[] i = result.Select(h => h.Id).ToArray();

                TempData["data"] = JsonConvert.SerializeObject(result);
                pouleId = CreateEmptyPoule(i);
            }
            else
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

            return RedirectToAction("Index","Game", new { id = pouleId });

        }

        private int NewPouleId()
        {
            Poule poule = new Poule
            {
                DateTime = DateTime.Now
            };

            var newPoule = _pouleRepository.Add(poule);


            return newPoule.Id;
        }

        private int CreateEmptyPoule(int[] teamIds)
        {
            int pouleId = NewPouleId();

            foreach (int home in teamIds)
            {
                foreach(int away in teamIds.Where(val => val != home).ToArray())
                {
                    CreateGame(home, away, pouleId);
                }
            }
            return pouleId;
        }

        private void CreateGame(int homeTeam, int awayTeam, int pouleId)
        {
            Game game = new Game
            {
                PouleId = pouleId,
                HomeTeamId = homeTeam, 
                AwayTeamId = awayTeam
            };

            _gameRepository.Add(game);
        }
    }
}
