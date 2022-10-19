namespace Task1.Models;

public class Product
{
    public Product() { }

    public Product(int id, string name, decimal price, double weight)
    {
        Id = id;
        Name = name;
        Price = price;
        Weight = weight;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public double Weight { get; set; }
    
    public override string ToString() =>
    $"{nameof(Id)}: {Id} | {nameof(Name)}: {Name} | {nameof(Price)}: {Price} | {nameof(Weight)}: {Weight}";
}