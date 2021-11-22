using System.ComponentModel.DataAnnotations;

namespace ApiGameCatalog.InputModel.Publisher
{
    public class PublisherInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage ="Publisher name shall have at least 2 letters (max: 100)")]
        public string Name { get; set; }
    }
}
