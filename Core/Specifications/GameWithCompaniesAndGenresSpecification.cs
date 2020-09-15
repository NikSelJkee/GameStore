using Core.Entities;
using Core.Helpers;
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
            ApplyPaging(gameParams.PageSize * (gameParams.IndexPage - 1), gameParams.PageSize);

            switch (gameParams.Sort)
            {
                case SortState.TitleDesc:
                    AddOrderByDescending(g => g.Title);
                    break;
                case SortState.PriceAsc:
                    AddOrderBy(g => g.Price);
                    break;
                case SortState.PriceDesc:
                    AddOrderByDescending(g => g.Price);
                    break;
                case SortState.CompanyAsc:
                    AddOrderBy(g => g.Company.Name);
                    break;
                case SortState.CompanyDesc:
                    AddOrderByDescending(g => g.Company.Name);
                    break;
                default:
                    AddOrderBy(g => g.Title);
                    break;
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
