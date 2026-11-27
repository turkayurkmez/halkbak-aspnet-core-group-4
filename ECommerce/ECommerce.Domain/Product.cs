using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain;

public class Product
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Ürün adı dolu olmalıdır")]
    [MinLength(2, ErrorMessage = "En az iki harf... ")]
    [MaxLength(200, ErrorMessage ="En fazla 200 karakter")]
    public string Name { get; set; }  = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }
    public virtual Category? Category { get; set; }
    
}
