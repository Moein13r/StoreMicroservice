using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Products.Models
{
    public class Product
    {       
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Name{ get; set; }
    }
}
