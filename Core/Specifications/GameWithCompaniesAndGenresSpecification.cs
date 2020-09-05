using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{
    public class GameWithCompaniesAndGenresSpecification : BaseSpecification<Game>
    {
        public GameWithCompaniesAndGenresSpecification()
        {
            AddInclude(g => g.Company);
            AddInclude(g => g.Genre);
        }

        public GameWithCompaniesAndGenresSpecification(int id)
            : base(x => x.Id == id)
        {
            AddInclude(g => g.Company);
            AddInclude(g => g.Genre);
        }
    }
}
