namespace Homework3_LiudvynskyiV.S.Models;

// Homework #5
public class Basket
{
    public IEnumerable<Product> Products { get; set; }

    public Basket(IEnumerable<Product> products)
    {
        
    }

    private bool IsCorrectCurrency()
    {
        var firstProductCurrency = Products.First().Currency;
        foreach (var product in Products)
        {
            if (product.Currency != firstProductCurrency)
            {
                return false;
            }
        }
        return true;
    }
    
    private bool IsCorrectWeightMeasure()
    {
        var firstWeightMeasure = Products.First().WeightMeasure;
        foreach (var product in Products)
        {
            if (product.WeightMeasure != firstWeightMeasure)
            {
                return false;
            }
        }
        return true;
    }

    public IEnumerable<Product> GetBasket()
    {
        if (IsCorrectCurrency() || IsCorrectWeightMeasure())
        {
            return Products;
        }

        throw new ArgumentException("In your basket not all products have " +
                                    "the same currencies or weight measures!");
    }
}