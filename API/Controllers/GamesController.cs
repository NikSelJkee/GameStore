using Core.Entities;
using Core.Services;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class GamesController : BaseApiController
    {
        private readonly IGenericRepository<Game> _gamesRepository;
        private readonly IGenericRepository<Company> _gameCompaniesRepository;
        private readonly IGenericRepository<Genre> _gameGenresRepository;

        public GamesController(IGenericRepository<Game> gamesRepository,
            IGenericRepository<Company> gameCompaniesRepository,
            IGenericRepository<Genre> gameGenresRepository)
        {
            _gamesRepository = gamesRepository;
            _gameCompaniesRepository = gameCompaniesRepository;
            _gameGenresRepository = gameGenresRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Game>>> GetGames()
        {
            var specification = new GameWithCompaniesAndGenresSpecification();
            var games = await _gamesRepository.GetAllWithSpecificationAsync(specification);

            return Ok(games);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(int id)
        {
            var specification = new GameWithCompaniesAndGenresSpecification(id);
            var game = await _gamesRepository.GetEntityWithSpecificationAsync(specification);

            return Ok(game);
        }

        [HttpGet("companies")]
        public async Task<ActionResult<IReadOnlyList<Company>>> GetGameCompanies()
        {
            var companies = await _gameCompaniesRepository.GetAllAsync();

            return Ok(companies);
        }

        [HttpGet("genres")]
        public async Task<ActionResult<IReadOnlyList<Genre>>> GetGameGenres()
        {
            var genres = await _gameGenresRepository.GetAllAsync();

            return Ok(genres);
        }
    }
}
