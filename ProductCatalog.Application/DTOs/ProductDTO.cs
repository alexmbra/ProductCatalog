using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Application.DTOs;
public class ProductDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The Name is Required")]
    [MinLength(3)]
    [MaxLength(100)]
    [DisplayName("Name")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "The Description is Required")]
    [MinLength(5)]
    [MaxLength(200)]
    [DisplayName("Description")]
    public string? Description { get; set; }


    [Required(ErrorMessage = "The Price is Required")]
    [Column(TypeName = "decimal(10,2)")]
    //[Range(1, 10999.00, ErrorMessage = "Valor deve ser maior do que 0")]
    [DisplayName("Price")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "The Stock is Required")]
    [Range(1, 999)]
    [DisplayName("Stock")]
    public int Stock { get; set; }

    [MaxLength(250)]
    [DisplayName("Product Image")]
    public string? Image { get; set; }

    [DisplayName("Categories")]
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}
