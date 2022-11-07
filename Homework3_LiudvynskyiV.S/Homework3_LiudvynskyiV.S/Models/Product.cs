using System.Collections;
using System.ComponentModel.DataAnnotations;
using Homework3_LiudvynskyiV.S.Enums;

namespace Homework3_LiudvynskyiV.S.Models;

public class Product : IComparable // Homework #4
{
    public Product(decimal Price, double Weight)
    {
        this.Price = Price;
        this.Weight = Weight;
    }
    
    [Range(1, 100, ErrorMessage = "Price must be between $1 and $100")]
    public decimal Price { get; set; }
    public Currencies Currency { get; set; }
    [Range(1, 10, ErrorMessage = "Weight could not be over 10 kilograms!")]
    public double Weight { get; set; }
    public WeightMeasures WeightMeasure { get; set; }
    

    public virtual void ChangePrice(double percent)
    {
        var coef = (decimal)(percent / 100);
        Price += Price * coef;
    }

    public int CompareTo(object? obj)
    {
        if (obj is Product product) return Price.CompareTo(product.Price);
        else throw new ArgumentException("Incorrect value!");
    }
}