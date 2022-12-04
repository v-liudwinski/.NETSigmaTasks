using Homework8_LiudvynskyiV.S.Models;

namespace Homework8_LiudvynskyiV.S.Services;

public interface IProductService
{
    List<Product> GetAvailableProducts();
    bool DoesProductWithCurrentNameExist(string name);
    Product? GetProductByName(string name);
}