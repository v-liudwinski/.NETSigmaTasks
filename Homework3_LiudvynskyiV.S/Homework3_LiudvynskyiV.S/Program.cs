using Homework3_LiudvynskyiV.S.Models;

var storage = new Storage();
storage.AddProducts(3);
storage.ChangePrices(25);

var check = new Check(storage.GetProducts());
check.ShowAllProducts();