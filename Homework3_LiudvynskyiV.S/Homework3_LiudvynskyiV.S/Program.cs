using Homework3_LiudvynskyiV.S.Enums;
using Homework3_LiudvynskyiV.S.Models;

var storage = new Storage();
storage.AddProducts(3);
storage.ChangePrices(25);
storage.SortProductsByPrice();
foreach (var product in storage.GetProducts())
{
    product.Currency = Currencies.Dollar;
    product.WeightMeasure = WeightMeasures.Kilogram;
}

var check = new Check(storage.GetProducts());
check.ShowAllProducts();

Console.WriteLine();
var basket = new Basket(storage.GetProducts());
basket.Print();