using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Entities
{
    public class Genre : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
