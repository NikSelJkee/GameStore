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
    }
}
