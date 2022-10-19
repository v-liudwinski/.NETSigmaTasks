namespace Task1.Models;

public class Check
{
    public Check(Buy buy)
    {
        _buy = buy;
    }

    private readonly Buy _buy;

    public void CheckOut()
    {
        var check = string.Join("\n", _buy.GetProductsWithQuantities(), _buy.CalculateTotalPrice());
        Console.WriteLine(check);
    }
}