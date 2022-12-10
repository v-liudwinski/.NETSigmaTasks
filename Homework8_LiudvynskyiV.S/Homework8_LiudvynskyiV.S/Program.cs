using Homework8_LiudvynskyiV.S.Models;
using Homework8_LiudvynskyiV.S.Services;

Action<string> Notify = Console.WriteLine;
var rootPath = @"D:\VisualStudio\.NETSigmaTasks\Homework8_LiudvynskyiV.S\request.txt";
var resultPath = @"D:\VisualStudio\.NETSigmaTasks\Homework8_LiudvynskyiV.S\response.txt";
var additionalProductsPath = @"D:\VisualStudio\.NETSigmaTasks\Homework8_LiudvynskyiV.S\additianal.txt";

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
IFileHandler fileHandler = new FileHandler(rootPath, resultPath, additionalProductsPath, productService);
var storageOperations = new StorageOperations();
fileHandler.PurchaseSuccessful += Notify;
fileHandler.PurchaseFailed += Notify;
storageOperations.OrderOperation = fileHandler.GetPurchases;
storageOperations.OrderOperation += fileHandler.CompleteTheOrder;
storageOperations.ExecuteOrdering += storageOperations.OrderOperation;
storageOperations.OnExecuteOrdering();