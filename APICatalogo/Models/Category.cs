using System.Collections.ObjectModel;

namespace APICatalogo.Models;

public class Category
{
    public Category()
    {
        Products = new Collection<Product>(); //initialize the collection
    }

    public int CategoryId { get; set; }
    public String? Name { get; set; }
    public string? ImageUrl { get; set; }
    public ICollection<Product>? Products { get; set; }

}
