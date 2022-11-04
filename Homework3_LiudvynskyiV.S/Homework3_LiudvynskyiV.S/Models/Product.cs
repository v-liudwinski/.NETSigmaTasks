namespace Homework3_LiudvynskyiV.S.Models;

public class Product
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
}