using Homework3_LiudvynskyiV.S.Models;

namespace Homework3_LiudvynskyiV.S.Services;

public class ProductsComparer : IStorageComparer
{
    public bool IsEqual(Product product1, Product product2)
    {
        if (product1.GetType() != product2.GetType()) return false;

        if (product1 is Meat && product2 is Meat)
        {
            return (product1 as Meat).Category == (product2 as Meat).Category 
                   && (product1 as Meat).Type == (product2 as Meat).Type;
        }
        
        return (product1 as DairyProducts).ExpirationDate == (product2 as DairyProducts).ExpirationDate;
    }
}