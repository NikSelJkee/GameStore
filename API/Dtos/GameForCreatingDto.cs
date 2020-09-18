using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class GameForCreatingDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(300)]
        public string Description { get; set; }
        [Column("decimal(18,2)")]
        public decimal Price { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        public int CompanyId { get; set; }
        public int GenreId { get; set; }
    }
}
