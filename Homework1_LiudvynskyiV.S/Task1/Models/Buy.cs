namespace Task1.Models;

public class Buy
{
    public Buy() { }

    public Buy(IEnumerable<Product> products)
    {
        Products = products;
    }
    
    public IEnumerable<Product> Products { get; set; }

    public decimal CalculateTotalPrice()
    {
        return Products.Sum(x => x.Price);
    }

    public string GetProductsWithQuantities()
    {
        return string.Join(
            "\n",
            Products.Select(x =>
            {
                return $"{x.ToString()}: X{Products.Count(p => p.Name == x.Name)}";
            }).Distinct()
        );
    }
}