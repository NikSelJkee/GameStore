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
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class GamesController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GamesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<GameToReturnDto>>> GetGames(
            [FromQuery]GameSpecificationParams gameParams)
        {
            var specification = new GameWithCompaniesAndGenresSpecification(gameParams);
            var countSpecification = new GameWithFilterForCountSpecification(gameParams);
            var totalItems = await _unitOfWork.Games.CountAsync(countSpecification);
            var data = await _unitOfWork.Games.GetAllWithSpecificationAsync(specification);
            var games = _mapper.Map<IReadOnlyList<GameToReturnDto>>(data);

            return Ok(new Pagination<GameToReturnDto>(gameParams.IndexPage, gameParams.PageSize, totalItems, games));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameToReturnDto>> GetGame(int id)
        {
            var specification = new GameWithCompaniesAndGenresSpecification(id);
            var game = await _unitOfWork.Games.GetEntityWithSpecificationAsync(specification);

            if (game == null)
                return NotFound(new ApiResponse(404, "Game Not Found"));

            return Ok(_mapper.Map<GameToReturnDto>(game));
        }

        [HttpGet("companies")]
        public async Task<ActionResult<IReadOnlyList<Company>>> GetGameCompanies()
        {
            var companies = await _unitOfWork.Games.GetAllAsync();

            return Ok(companies);
        }

        [HttpGet("genres")]
        public async Task<ActionResult<IReadOnlyList<Genre>>> GetGameGenres()
        {
            var genres = await _unitOfWork.Games.GetAllAsync();

            return Ok(genres);
        }

        [HttpPost]
        public async Task<ActionResult<GameToReturnDto>> CreateGame(
            [FromBody]GameForCreatingDto gameForCreating)
        {
            if (!await _unitOfWork.Companies.ExistsAsync(gameForCreating.CompanyId))
                return NotFound(new ApiResponse(404, "Company Not Found"));

            if (!await _unitOfWork.Genres.ExistsAsync(gameForCreating.GenreId))
                return NotFound(new ApiResponse(404, "Genre Not Found"));

            var game = _mapper.Map<Game>(gameForCreating);

            await _unitOfWork.Games.AddAsync(game);
            await _unitOfWork.SaveAsync();

            var specification = new GameWithCompaniesAndGenresSpecification(game.Id);
            var gameToReturn = await _unitOfWork.Games.GetEntityWithSpecificationAsync(specification);

            return Ok(_mapper.Map<GameToReturnDto>(gameToReturn));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GameToReturnDto>> UpdateGame(int id, 
            [FromBody]GameForUpdatingDto gameForUpdating)
        {
            if (!await _unitOfWork.Companies.ExistsAsync(gameForUpdating.CompanyId))
                return NotFound(new ApiResponse(404, "Company Not Found"));

            if (!await _unitOfWork.Genres.ExistsAsync(gameForUpdating.GenreId))
                return NotFound(new ApiResponse(404, "Genre Not Found"));

            if (gameForUpdating.Id != id)
                return BadRequest(new ApiResponse(400));

            var game = _mapper.Map<Game>(gameForUpdating);

            _unitOfWork.Games.Update(game);
            await _unitOfWork.SaveAsync();

            var specification = new GameWithCompaniesAndGenresSpecification(game.Id);
            var gameToReturn = await _unitOfWork.Games.GetEntityWithSpecificationAsync(specification);

            return Ok(_mapper.Map<GameToReturnDto>(gameToReturn));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var specification = new GameWithCompaniesAndGenresSpecification(id);
            var game = await _unitOfWork.Games.GetEntityWithSpecificationAsync(specification);

            if (game == null)
                return NotFound(new ApiResponse(404, "Game Not Found"));

            _unitOfWork.Games.Remove(game);
            await _unitOfWork.SaveAsync();

            return Ok();
        }
    }
}
