using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{
    public class GameWithCompaniesAndGenresSpecification : BaseSpecification<Game>
    {
        public GameWithCompaniesAndGenresSpecification(GameSpecificationParams gameParams) 
            : base(x =>
                (string.IsNullOrEmpty(gameParams.Search) || x.Title.ToLower().Contains(gameParams.Search)) &&
                (!gameParams.CompanyId.HasValue || x.CompanyId == gameParams.CompanyId) &&
                (!gameParams.GenreId.HasValue || x.GenreId == gameParams.GenreId)
            )
        {
            AddInclude(g => g.Company);
            AddInclude(g => g.Genre);
            AddOrderBy(g => g.Title);

            if (!string.IsNullOrEmpty(gameParams.Sort))
            {
                switch (gameParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(g => g.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(g => g.Price);
                        break;
                }
            }
        }

        public GameWithCompaniesAndGenresSpecification(int id)
            : base(x => x.Id == id)
        {
            AddInclude(g => g.Company);
            AddInclude(g => g.Genre);
        }
    }
}
