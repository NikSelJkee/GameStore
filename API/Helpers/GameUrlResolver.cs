using API.Dtos;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class GameUrlResolver : IValueResolver<Game, GameToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public GameUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Game source, GameToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _configuration["ApiUrl"] + source.PictureUrl;
            }

            return null;
        }
    }
}
