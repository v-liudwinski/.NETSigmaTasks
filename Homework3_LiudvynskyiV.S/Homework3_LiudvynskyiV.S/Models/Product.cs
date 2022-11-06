using System.Collections;

namespace Homework3_LiudvynskyiV.S.Models;

public class Product : IComparable // Homework #4
{
    public Product(double Price)
    {
        this.Price = Price;
    }

    public double Price { get; set; }

    public virtual void ChangePrice(double percent)
    {
        var coef = percent / 100;
        Price += Price * coef;
    }

    public int CompareTo(object? obj)
    {
        if (obj is Product product) return Price.CompareTo(product.Price);
        else throw new ArgumentException("Incorrect value!");
    }
}