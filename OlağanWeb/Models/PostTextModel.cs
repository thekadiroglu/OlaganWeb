using System.ComponentModel.DataAnnotations;

namespace OlağanWeb.Models
{
    public class PostTextModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Writer { get; set; }
        [Required]
        public string number { get; set; }
        [Required]
        public string picture { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string Referances { get; set; }
        [Required]
        public string Summary { get; set; }
        [Required]
        public bool PublishedInOlaganSiir { get; set; }
    }
}
