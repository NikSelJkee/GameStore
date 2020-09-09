using API.Controllers;
using Core.Entities;
using Core.Services;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace API.Tests
{
    public class IntegrationTest
    {
        protected readonly HttpClient _testClient;

        public IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            _testClient = appFactory.CreateClient();
        }
    }
}
