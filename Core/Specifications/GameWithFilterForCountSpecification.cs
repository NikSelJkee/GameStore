using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{
    public class GameWithFilterForCountSpecification : BaseSpecification<Game>
    {
        public GameWithFilterForCountSpecification(GameSpecificationParams gameParams)
            : base(x =>
                (string.IsNullOrEmpty(gameParams.Search) || x.Title.ToLower().Contains(gameParams.Search)) &&
                (!gameParams.CompanyId.HasValue || x.CompanyId == gameParams.CompanyId) &&
                (!gameParams.GenreId.HasValue || x.GenreId == gameParams.GenreId)
            )
        {

        }
    }
}
