using Core.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Companies.Any())
                {
                    var companies = new List<Company>
                    {
                        new Company { Name = "Bethesda" },
                        new Company { Name = "From Software" },
                        new Company { Name = "Bandai Namco" }
                    };

                    context.Companies.AddRange(companies);
                    await context.SaveChangesAsync();
                }

                if (!context.Genres.Any())
                {
                    var genres = new List<Genre>
                    {
                        new Genre { Name = "RPG"},
                        new Genre { Name = "Action"},
                        new Genre { Name = "JRPG"}
                    };

                    context.Genres.AddRange(genres);
                    await context.SaveChangesAsync();
                }

                if (!context.Games.Any())
                {
                    var games = new List<Game>
                    {
                        new Game { Title = "The Elder Scrolls III: Morrowind", Description = "RPG by Bethesda", Price = 400, PictureUrl = "images/games/morrowind.png", CompanyId = 1, GenreId = 3},
                        new Game { Title = "The Elder Scrolls IV: Oblivion", Description = "RPG by Bethesda", Price = 600, PictureUrl = "images/games/oblivion.png", CompanyId = 1, GenreId = 3},
                        new Game { Title = "The Elder Scrolls V: Skyrim", Description = "RPG by Bethesda", Price = 800, PictureUrl = "images/games/skyrim.png", CompanyId = 1, GenreId = 3},
                        new Game { Title = "Dark Souls", Description = "RPG by From Software", Price = 1000, PictureUrl = "images/games/dark_souls.png", CompanyId = 2, GenreId = 2},
                        new Game { Title = "Dark Souls 2", Description = "RPG by From Software", Price = 1200, PictureUrl = "images/games/dark_souls_2.png", CompanyId = 2, GenreId = 2},
                        new Game { Title = "Dark Souls 3", Description = "RPG by From Software", Price = 1400, PictureUrl = "images/games/dark_souls_3.png", CompanyId = 2, GenreId = 2},
                        new Game { Title = "Tales of Zestiria", Description = "RPG by Bandai Namco", Price = 1600, PictureUrl = "images/games/tales_of_zestiria.png", CompanyId = 3, GenreId = 1},
                        new Game { Title = "Tales of Berseria", Description = "RPG by Bandai Namco", Price = 1800, PictureUrl = "images/games/tales_of_berseria.png", CompanyId = 3, GenreId = 1},
                        new Game { Title = "Tales of Vesperia", Description = "RPG by Bandai Namco", Price = 2000, PictureUrl = "images/games/tales_of_vesperia.png", CompanyId = 3, GenreId = 1}
                    };

                    context.Games.AddRange(games);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<ILogger<StoreContextSeed>>();
                logger.LogError(ex, ex.Message);
            }
        }
    }
}
