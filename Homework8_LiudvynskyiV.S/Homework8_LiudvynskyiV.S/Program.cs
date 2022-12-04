// See https://aka.ms/new-console-template for more information

using Homework8_LiudvynskyiV.S.Models;
using Homework8_LiudvynskyiV.S.Services;

Action<string> Notify = Console.WriteLine;
var rootPath = @"(your_path)\request.txt";
var resultPath = @"(your_path)\response.txt";

var products = new List<Product>
{
    new Product
    {
        Name = "chicken",
        Price = 5.5m,
        Quantity = 10
    },
    new Product
    {
        Name = "bread",
        Price = 1.5m,
        Quantity = 10
    },
    new Product
    {
        Name = "cheese",
        Price = 2.5m,
        Quantity = 10
    }
};
IProductService productService = new ProductService(products);
IFileHandler fileHandler = new FileHandler(rootPath, resultPath, productService);
var storageOperations = new StorageOperations();
fileHandler.PurchaseSuccessful += Notify;
fileHandler.PurchaseFailed += Notify;
storageOperations.OrderOperation = fileHandler.GetPurchases;
storageOperations.OrderOperation += fileHandler.CompleteTheOrder;
storageOperations.ExecuteOrdering += storageOperations.OrderOperation;
storageOperations.OnExecuteOrdering();