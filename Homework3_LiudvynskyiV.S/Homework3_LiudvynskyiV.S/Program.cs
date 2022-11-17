using Homework3_LiudvynskyiV.S.Enums;
using Homework3_LiudvynskyiV.S.Models;
using Homework3_LiudvynskyiV.S.Services;

var storage1 = new Storage();
storage1.AddProducts(3);
storage1.ChangePrices(25);
storage1.SortProductsByPrice();
foreach (var product in storage1.GetProducts())
{
    product.Currency = Currencies.Dollar;
    product.WeightMeasure = WeightMeasures.Kilogram;
}

var check = new Check(storage1.GetProducts());
check.ShowAllProducts();

Console.WriteLine();
var basket = new Basket(storage1.GetProducts());
basket.Print();

var storage2 = new Storage();
storage2.AddProducts(3);
storage2.ChangePrices(25);
storage2.SortProductsByPrice();
foreach (var product in storage2.GetProducts())
{
    product.Currency = Currencies.Dollar;
    product.WeightMeasure = WeightMeasures.Kilogram;
}

var check2 = new Check(storage2.GetProducts());
check.ShowAllProducts();

Console.WriteLine();
var basket2 = new Basket(storage2.GetProducts());
basket.Print();

var storageComparer = new StorageComparer(storage1, storage2);
Console.WriteLine();
StorageComparer.Print(storageComparer.LeftCompare());
Console.WriteLine();
StorageComparer.Print(storageComparer.InnerCompare());
Console.WriteLine();
StorageComparer.Print(storageComparer.UniqueInnerCompare());