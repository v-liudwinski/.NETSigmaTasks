using Homework8_LiudvynskyiV.S.Models;

namespace Homework8_LiudvynskyiV.S.Services;

public class ProductService : IProductService
{
    private readonly List<Product> _products;

    public ProductService(List<Product> products)
    {
        _products = products;
    }

    public List<Product> GetAvailableProducts() => _products;

    public bool DoesProductWithCurrentNameExist(string name) => _products.Any(x => x.Name == name);
    public Product? GetProductByName(string name) => _products.FirstOrDefault(x => x.Name == name);
}