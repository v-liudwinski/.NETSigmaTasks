namespace Homework8_LiudvynskyiV.S.Services;

public interface IFileHandler
{
    event Action<string> PurchaseSuccessful;
    event Action<string> PurchaseFailed;
    void GetPurchases();
    void CompleteTheOrder();
}