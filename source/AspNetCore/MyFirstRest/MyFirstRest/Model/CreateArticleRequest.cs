using System.ComponentModel.DataAnnotations;

namespace MyFirstRest.Model
{
    public class CreateArticleRequest
    {
        [StringLength(50)] public string Number { get; set; }

        [StringLength(100)] public string Name { get; set; }

        public decimal Price { get; set; }

        [MaxLength] public string Description { get; set; }

        [MaxLength] public string ImageUrl { get; set; }
    }
}