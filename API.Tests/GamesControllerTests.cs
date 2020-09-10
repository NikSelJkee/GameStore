using API.Dtos;
using API.Helpers;
using Core.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Tests
{
    public class GamesControllerTests : IntegrationTest
    {
        [Test]
        public async Task GetAllGames_ShouldReturnAllGames()
        {
            string requestUrl = "http://localhost:5000/api/games";

            var response = await _testClient.GetAsync(requestUrl);
            var pagination = await _testClient.GetJsonAsync<Pagination<GameToReturnDto>>(requestUrl);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(pagination);
            Assert.AreEqual(pagination.PageSize, pagination.Data.Count);
        }

        [Test]
        public async Task GetGame_ShouldReturnGameById()
        {
            int id = 1;
            string requestUrl = "http://localhost:5000/api/games/" + id;

            var response = await _testClient.GetAsync(requestUrl);
            var game = await _testClient.GetJsonAsync<GameToReturnDto>(requestUrl);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(game);
            Assert.AreEqual(id, game.Id);
        }

        [Test]
        public async Task GetGameCompanies_ShouldReturnAllGameCompanies()
        {
            string requestUrl = "http://localhost:5000/api/games/companies";

            var response = await _testClient.GetAsync(requestUrl);
            var companies = await _testClient.GetJsonAsync<IReadOnlyList<Company>>(requestUrl);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(companies);
        }

        [Test]
        public async Task GetGameGenres_ShouldReturnAllGameGenres()
        {
            string requestUrl = "http://localhost:5000/api/games/genres";

            var response = await _testClient.GetAsync(requestUrl);
            var companies = await _testClient.GetJsonAsync<IReadOnlyList<Genre>>(requestUrl);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(companies);
        }

        [Test]
        public async Task CreateGame_ShouldReturnCreatedGame()
        {
            string requestUrl = "http://localhost:5000/api/games";
            string imageUrl = "http://localhost:5000";
            GameForCreatingDto gameForCreating = new GameForCreatingDto
            {
                Title = "New Game",
                Description = "New Game by Company",
                Price = 1000,
                PictureUrl = "images/games/new_game.png",
                CompanyId = 1,
                GenreId = 1
            };

            var companies = await _testClient.GetJsonAsync<List<Company>>(requestUrl + "/companies");
            var genres = await _testClient.GetJsonAsync<List<Genre>>(requestUrl + "/genres");

            var companyForCreatedGame = companies.Find(x => x.Id == gameForCreating.CompanyId);
            var genreForCreatedGame = genres.Find(x => x.Id == gameForCreating.GenreId);

            var createdGame = await _testClient.PostJsonAsync<GameToReturnDto>(requestUrl, gameForCreating);

            Assert.AreEqual("New Game", createdGame.Title);
            Assert.AreEqual("New Game by Company", createdGame.Description);
            Assert.AreEqual(1000, createdGame.Price);
            Assert.AreEqual(imageUrl + "/images/games/new_game.png", createdGame.PictureUrl);
            Assert.AreEqual(companyForCreatedGame.Name, createdGame.Company);
            Assert.AreEqual(genreForCreatedGame.Name, createdGame.Genre);
        }
    }
}
