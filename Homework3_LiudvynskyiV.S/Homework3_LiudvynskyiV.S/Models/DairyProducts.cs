namespace Homework3_LiudvynskyiV.S.Models;

public class DairyProducts : Product
{
    public DairyProducts(decimal Price, double Weight) : base(Price, Weight)
    {
    }

    public DateTime ExpirationDate { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is null || GetType() != obj.GetType()) return false;

        var dairyProducts = obj as DairyProducts;
        return ExpirationDate == dairyProducts.ExpirationDate;
    }
    
    public override int GetHashCode()
    {
        return ExpirationDate.GetHashCode();
    }

    public override string ToString()
    {
        return $"{nameof(ExpirationDate)}: {ExpirationDate} | {nameof(Price)}: {Price}";
    }

    public override void ChangePrice(double percent)
    {
        base.ChangePrice(percent);
        if ((DateTime.Now - ExpirationDate).Days >= 30)
        {
            Price += Price * 0.3m;
        }
        else if ((DateTime.Now - ExpirationDate).Days >= 7)
        {
            Price += Price * 0.1m;
        }
    }
}