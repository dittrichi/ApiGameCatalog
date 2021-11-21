using System;
using System.ComponentModel.DataAnnotations;

namespace ApiGameCatalog.InputModel
{
    public class GameInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage ="Game name shall have between 3 and 100 chars")]
        public string Name { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Publisher name shall have between 1 and 100 chars")]
        public string Publisher { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "Price shall be between 1 and 1000")]
        public double Price { get; set; }
    }
}
