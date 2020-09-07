using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public GamesController(IGenericRepository<Game> gamesRepository,
            IGenericRepository<Company> gameCompaniesRepository,
            IGenericRepository<Genre> gameGenresRepository,
            IMapper mapper)
        {
            _gamesRepository = gamesRepository;
            _gameCompaniesRepository = gameCompaniesRepository;
            _gameGenresRepository = gameGenresRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<GameToReturnDto>>> GetGames(
            [FromQuery]GameSpecificationParams gameParams)
        {
            var specification = new GameWithCompaniesAndGenresSpecification(gameParams);
            var countSpecification = new GameWithFilterForCountSpecification(gameParams);
            var totalItems = await _gamesRepository.CountAsync(countSpecification);
            var data = await _gamesRepository.GetAllWithSpecificationAsync(specification);
            var games = _mapper.Map<IReadOnlyList<GameToReturnDto>>(data);

            return Ok(new Pagination<GameToReturnDto>(gameParams.IndexPage, gameParams.PageSize, totalItems, games));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameToReturnDto>> GetGame(int id)
        {
            var specification = new GameWithCompaniesAndGenresSpecification(id);
            var game = await _gamesRepository.GetEntityWithSpecificationAsync(specification);

            if (game == null)
                return NotFound(new ApiResponse(404, "Game Not Found"));

            return Ok(_mapper.Map<GameToReturnDto>(game));
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

        [HttpPost]
        public async Task<ActionResult<GameToReturnDto>> CreateGame([FromBody]GameForCreatingDto gameForCreating)
        {
            if (!await _gameCompaniesRepository.ExistsAsync(gameForCreating.CompanyId))
                return NotFound(new ApiResponse(404, "Company Not Found"));

            if (!await _gameGenresRepository.ExistsAsync(gameForCreating.GenreId))
                return NotFound(new ApiResponse(404, "Genre Not Found"));

            var game = _mapper.Map<Game>(gameForCreating);

            await _gamesRepository.AddAsync(game);
            await _gamesRepository.SaveAsync();

            var specification = new GameWithCompaniesAndGenresSpecification(game.Id);
            var gameToReturn = await _gamesRepository.GetEntityWithSpecificationAsync(specification);

            return Ok(_mapper.Map<GameToReturnDto>(gameToReturn));
        }
    }
}
