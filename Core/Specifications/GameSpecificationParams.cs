using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{
    public class GameSpecificationParams
    {
        public const int MaxPageSize = 50;

        public int IndexPage { get; set; } = 1;

        private int _pageSize = 6;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public SortState Sort { get; set; }

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
