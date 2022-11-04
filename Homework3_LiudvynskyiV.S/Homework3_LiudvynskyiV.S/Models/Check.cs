namespace Homework3_LiudvynskyiV.S.Models;

public class Check
{
    private readonly Product[] _products;

    public Check(Product[] products)
    {
        _products = products;
    }

    public void ShowAllProducts()
    {
        foreach (var product in _products)
        {
            Console.WriteLine(product.ToString());
        }
    }
}