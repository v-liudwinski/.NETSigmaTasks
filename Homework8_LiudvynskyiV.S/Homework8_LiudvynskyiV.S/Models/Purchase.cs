namespace Homework8_LiudvynskyiV.S.Models;

public class Purchase
{
    public string FirmName { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }

    public override string ToString()
    {
        return $"Client: {FirmName} | Product: {ProductName} | Quantity: {Quantity}";
    }
}