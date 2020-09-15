using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Helpers
{
    public class PaginationParams
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPagingEnabled { get; set; }
    }
}
