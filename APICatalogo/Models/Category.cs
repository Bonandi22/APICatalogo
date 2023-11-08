using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace APICatalogo.Models;

public class Category
{
    public Category()
    {
        Products = new Collection<Product>(); //initialize the collection
    }

    [Key]
    public int CategoryId { get; set; }

    [Required]
    [StringLength(80)]
    public String? Name { get; set; }

    [Required]
    [StringLength(300)]
    public string? ImageUrl { get; set; }
    public ICollection<Product>? Products { get; set; }

}
