using System.ComponentModel.DataAnnotations;

namespace Products.DTOs
{
    public class ProductUpdate
    {
        [Key]        
        public int Id { get; set; }

        [MaxLength(10)]
        [Required]
        public string Name { get; set; }
    }
}
