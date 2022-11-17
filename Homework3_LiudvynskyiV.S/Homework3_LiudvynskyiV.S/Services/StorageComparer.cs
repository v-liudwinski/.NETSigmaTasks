using Homework3_LiudvynskyiV.S.Models;

namespace Homework3_LiudvynskyiV.S.Services;

public class StorageComparer
{
    private readonly Storage _firstStorage;
    private readonly Storage _secondStorage;
    private readonly IStorageComparer _storageComparer = new ProductsComparer();

    public StorageComparer(Storage firstStorage, Storage secondStorage)
    {
        _firstStorage = firstStorage;
        _secondStorage = secondStorage;
    }

    public List<Product> LeftCompare()
    {
        var products = new List<Product>();
        var veify = true;
        foreach (var product1 in _firstStorage.GetProducts())
        {
            if (_secondStorage.GetProducts()
                .Any(product2 => _storageComparer
                    .IsEqual(product1, product2)))
            {
                veify = false;
            }

            if (veify)
            {
                products.Add(product1);
            }

            veify = true;
        }

        return products;
    }

    public List<Product> InnerCompare()
    {
        var products = _firstStorage.GetProducts()
            .Where(product1 => 
                _secondStorage.GetProducts()
                .Any(product2 => _storageComparer.IsEqual(product1, product2)))
            .ToList();

        return products;
    }

    public List<Product> UniqueInnerCompare()
    {
        return InnerCompare().Distinct().ToList();
    }

    public static void Print(List<Product> products)
    {
        foreach (var product in products)
        {
            Console.WriteLine(product.ToString());
        }
    }
}