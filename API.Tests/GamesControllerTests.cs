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
            Pagination<GameToReturnDto> pagination = new Pagination<GameToReturnDto>();
            string requestUrl = "http://localhost:5000/api/games";

            var response = await _testClient.GetAsync(requestUrl);
            pagination = await _testClient.GetJsonAsync<Pagination<GameToReturnDto>>(requestUrl);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(pagination);
            Assert.AreEqual(pagination.PageSize, pagination.Data.Count);
        }

        [Test]
        public async Task GetGame_ShouldReturnGameById()
        {
            GameToReturnDto game = new GameToReturnDto();
            int id = 1;
            string requestUrl = "http://localhost:5000/api/games/" + id;

            var response = await _testClient.GetAsync(requestUrl);
            game = await _testClient.GetJsonAsync<GameToReturnDto>(requestUrl);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(game);
            Assert.AreEqual(id, game.Id);
        }
    }
}
