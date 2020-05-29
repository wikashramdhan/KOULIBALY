using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KOULIBALY.Models.InterfaceRepository;
using KOULIBALY.Models.Objects;
using KOULIBALY.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KOULIBALY.Controllers
{
    public class GameController : Controller
    {
        private readonly IPouleRepository _pouleRepository;
        private readonly IGameRepository _gameRepository;

        public GameController(IPouleRepository pouleRepository, IGameRepository gameRepository)
        {
            _pouleRepository = pouleRepository;
            _gameRepository = gameRepository;
        }
        public IActionResult Index(int id)
        {
            Poule poule = _pouleRepository.Get(id);
            List<GameResultViewModel> gameResultViewModel = new List<GameResultViewModel>();
            foreach (Game game in poule.Games.Where(a => a.PouleId == id)
                                             .GroupBy(a => a.HomeTeam)
                                             .Select(x => x.FirstOrDefault())
                                             .OrderBy(x => x.HomeTeam.Name))
            {
                gameResultViewModel.Add
                    (
                        new GameResultViewModel()
                        {
                            TeamId = game.HomeTeamId,
                            TeamName = game.HomeTeam.Name,
                            Points = 0,
                            GoalsFor = 0,
                            GoalsAgainst = 0,
                            GoalDifference = 0
                        }
                    );
            }
            

            return View(gameResultViewModel);

        }

        public IActionResult Results(string id)
        {
            List<GameResultViewModel> gameResultViewModel = new List<GameResultViewModel>();

            Poule poule = _pouleRepository.Get(Convert.ToInt32(id));
            List<Game> games = poule.Games.Where(a => a.PouleId == Convert.ToInt32(id)).ToList();
            foreach (Game g in games)
            {
                GenerateGameResult(g.Id);
            }



            foreach (Game g in games)
            {
                GenerateGameResult(g.Id);

                GameResultViewModel gameResult = gameResultViewModel.Find(a => a.TeamId == g.HomeTeamId);
                if(gameResult != null)
                {
                    gameResult.Points += (g.HomeScore.Value > g.AwayScore.Value) ? 3 : (g.HomeScore.Value < g.AwayScore.Value) ? 0 : 1;
                    gameResult.GoalsFor += g.HomeScore.Value;
                    gameResult.GoalsAgainst += g.AwayScore.Value;
                    gameResult.GoalDifference += g.HomeScore.Value - g.AwayScore.Value;
                }
                else
                {
                    gameResultViewModel.Add(
                        new GameResultViewModel()
                        {
                            TeamId = g.HomeTeamId,
                            TeamName = g.HomeTeam.Name,
                            Points = (g.HomeScore.Value > g.AwayScore.Value) ? 3 : (g.HomeScore.Value < g.AwayScore.Value) ? 0 : 1,
                            GoalsFor = g.HomeScore.Value,
                            GoalsAgainst = g.AwayScore.Value,
                            GoalDifference = g.HomeScore.Value - g.AwayScore.Value
                        }
                    );
                }
            }


            ViewBag.Games = games;

            return View(gameResultViewModel.OrderByDescending(a => a.Points).ThenByDescending(a => a.GoalDifference).ThenByDescending(a => a.GoalsFor).ThenBy(a => a.GoalsAgainst).ToList());
        }
        
        private Game GenerateGameResult(int id)
        {
            Game game = _gameRepository.Get(id);
            int strengthHomeTeam = game.HomeTeam.Strength;
            int strengthAwayTeam = game.AwayTeam.Strength;

            int chanceToWin = strengthHomeTeam;
            int chanceToLose = (strengthHomeTeam - strengthAwayTeam < 0) ? 1 : strengthHomeTeam - strengthAwayTeam;
            int chanceForDraw = chanceToWin - chanceToLose;

            Random random = new Random();
            
            int wins = random.Next(0, chanceToWin);
            int loses = random.Next(0, chanceToLose);
            int draws = random.Next(0, chanceForDraw);

            int homeScore = 0;
            int awayScore = 0;
            
            if(wins > draws && wins > loses)
            {
                homeScore = random.Next(1, 5);
                awayScore = random.Next(0, homeScore);
            }
            else if(loses > draws && loses > wins)
            {
                awayScore = random.Next(1, 5);
                homeScore = random.Next(0, awayScore);

            }
            else
            {
                int drawscore = random.Next(0, 5);
                awayScore = drawscore;
                homeScore = drawscore;
            }

            game.HomeScore = homeScore;
            game.AwayScore = awayScore;
            _gameRepository.Update(game);

            return game;
        }
    }
}