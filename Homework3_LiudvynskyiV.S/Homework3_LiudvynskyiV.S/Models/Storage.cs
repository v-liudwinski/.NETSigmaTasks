using Homework3_LiudvynskyiV.S.Enums;

namespace Homework3_LiudvynskyiV.S.Models;

public class Storage
{
    private Product[] _products;

    public Storage(Product[] products)
    {
        _products = products;
    }
    
    public Storage()
    {
    }

    public Product this[int index]
    {
        get => _products[index];
        set => _products[index] = value;
    }

    public void AddProducts(int count)
    {
        var products = new Product[count];
        for (int i = 0; i < count; i++)
        {
            Console.WriteLine("Choose the product:\n" +
                              "1. Meat\n" +
                              "2. Dairy products");
            Console.WriteLine("Put the number: ");
            var answer = Console.ReadLine();
            switch (answer)
            {
                case "1":
                    Console.Write("Put the price: ");
                    var priceStr = Console.ReadLine();
                    double.TryParse(priceStr, out double meatPrice);
                    products[i] = new Meat(meatPrice);
                    
                    
                    Console.WriteLine("Choose the category:\n" +
                                      "1. Premium\n" +
                                      "2. Second");
                    Console.WriteLine("Put the number: ");
                    answer = Console.ReadLine();

                    switch (answer)
                    {
                        case "1":
                            (products[i] as Meat).Category = Categories.Premium;
                            break;
                        case "2":
                            (products[i] as Meat).Category = Categories.Second;
                            break;
                        default:
                            break;
                    }
                    
                    Console.WriteLine("Choose the type:\n" +
                                      "1. Lamb\n" +
                                      "2. Veal\n" +
                                      "3. Pork\n" +
                                      "4. Chicken");
                    
                    Console.WriteLine("Put the number: ");
                    answer = Console.ReadLine();

                    switch (answer)
                    {
                        case "1":
                            (products[i] as Meat).Type = Types.Lamb;
                            break;
                        case "2":
                            (products[i] as Meat).Type = Types.Veal;
                            break;
                        case "3":
                            (products[i] as Meat).Type = Types.Pork;
                            break;
                        case "4":
                            (products[i] as Meat).Type = Types.Chicken;
                            break;
                        default:
                            break;
                    }
                    
                    break;
                case "2":
                    Console.Write("Put the price: ");
                    priceStr = Console.ReadLine();
                    double.TryParse(priceStr, out double dpPrice);
                    products[i] = new DairyProducts(dpPrice);
                    
                    Console.Write("Put the year: ");
                    int.TryParse(Console.ReadLine(), out int year);
                    
                    Console.Write("Put the month: ");
                    int.TryParse(Console.ReadLine(), out int month);
                    
                    Console.Write("Put the day: ");
                    int.TryParse(Console.ReadLine(), out int day);
                    
                    (products[i] as DairyProducts).ExpirationDate = new DateTime(year, month, day);
                    break;
                default:
                    break;
            }
        }

        _products = new Product[count];
        for (int i = 0; i < count; i++)
        {
            _products[i] = products[i];
        }
    }

    public Meat[] GetAllMeatProducts()
    {
        var products = new List<Meat>();
        foreach (var product in _products)
        {
            if (product is Meat)
            {
                products.Add((Meat)product);
            }
        }

        return products.ToArray();
    }

    public void ChangePrices(double percent)
    {
        foreach (var product in _products)
        {
            product.ChangePrice(percent);
        }
    }

    public Product[] GetProducts() => _products;
}