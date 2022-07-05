using System.ComponentModel.DataAnnotations;

namespace Products.DTOs
{
    public class ProductCreate
    {
        [MaxLength(10)]
        [Required]
        public string Name { get; set; }
    }
}
