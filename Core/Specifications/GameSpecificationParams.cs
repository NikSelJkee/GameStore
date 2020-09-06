using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{
    public class GameSpecificationParams
    {
        public string Sort { get; set; }

        public int? CompanyId { get; set; }
        public int? GenreId { get; set; }

        private string _search;
        public string Search
        {
            get => _search;
            set => _search = value.ToLower();
        }
    }
}
