using Homework3_LiudvynskyiV.S.Enums;

namespace Homework3_LiudvynskyiV.S.Models;

public class Meat : Product
{
    public Meat(decimal Price, double Weight) : base(Price, Weight)
    {
    }

    public Categories Category { get; set; }
    public Types Type { get; set; }

    public override bool Equals(object? obj)
    {
        if ((obj == null) || GetType() != obj.GetType()) return false;
        
        var meat = obj as Meat;
        return Category == meat.Category && Type == meat.Type;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)Category, (int)Type);
    }

    public override string ToString()
    {
        return $"{nameof(Category)}: {Category} | {nameof(Type)}: {Type} | {nameof(Price)}: {Price}";
    }

    public override void ChangePrice(double percent)
    {
        base.ChangePrice(percent);
        Price += (Category.GetHashCode() / 100) * Price;
    }
}