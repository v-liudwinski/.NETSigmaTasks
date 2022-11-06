using System.Collections;

namespace Homework3_LiudvynskyiV.S.Models;

// Homework #4
public class ProductComparer : IComparer
{
    public int Compare(object? x, object? y)
    {
        if (x is null || y is null || x is not Product || y is not Product)
        {
            throw new ArgumentException("Incorrect value!");
        }
        var firstProduct = x as Product;
        var secondProduct = y as Product;
        
        return (int)(firstProduct.Price - secondProduct.Price);
    }
}