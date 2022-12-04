using System.Text;
using Homework8_LiudvynskyiV.S.Models;

namespace Homework8_LiudvynskyiV.S.Services;

public class FileHandler : IFileHandler
{
    public event Action<string> PurchaseSuccessful;
    public event Action<string> PurchaseFailed;
    private readonly IProductService _productService;
    private readonly string rootPath;
    private readonly string resultPath;
    private List<Purchase> _purchases;

    public FileHandler(string rootPath, string resultPath, IProductService productService)
    {
        this.rootPath = rootPath;
        this.resultPath = resultPath;
        _productService = productService;
    }

    public void GetPurchases()
    {
        var file = File.ReadAllLines(rootPath);
        _purchases = file.Select(x => x
            .Split(','))
            .Select(x => new Purchase
            {
                FirmName = x[0],
                ProductName = x[1].ToLower(),
                Quantity = int.TryParse(x[2], out var qnt) ? qnt : 0
            })
            .Where(x => x.Quantity > 0 
                        && !string.IsNullOrWhiteSpace(x.ProductName) 
                        && !string.IsNullOrWhiteSpace(x.FirmName))
            .ToList();
    }

    public void CompleteTheOrder()
    {
        if (ArePurchasesCorrect())
        {
            _purchases.ForEach(x => _productService
                .GetProductByName(x.ProductName)!.Quantity -= x.Quantity);
            var totalPrice = _purchases
                .Select(x => _productService
                    .GetProductByName(x.ProductName)!.Price)
                .Sum();
            var result = new StringBuilder();
            foreach (var purchase in _purchases)
            {
                result.AppendLine(purchase.ToString());
            }
            File.WriteAllText(resultPath, result.ToString());
            PurchaseSuccessful($"Order completed successfully!\n" +
                               $"Total price: {totalPrice}");
        }
        else
        {
            PurchaseFailed("Order is invalid.");
        }
    }

    private bool ArePurchasesCorrect()
    {
        return _purchases.All(x =>
        {
            if (_productService.DoesProductWithCurrentNameExist(x.ProductName))
            {
                return _productService
                    .GetProductByName(x.ProductName)!.Quantity > x.Quantity;
            }
            return false;
        }) 
               && _purchases.All(x => _productService.DoesProductWithCurrentNameExist(x.ProductName));
    }
}