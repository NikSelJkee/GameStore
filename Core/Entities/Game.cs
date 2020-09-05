using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Game : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public Company Company { get; set; }
        public int CompanyId { get; set; }
        public Genre Genre { get; set; }
        public int GenreId { get; set; }
    }
}
