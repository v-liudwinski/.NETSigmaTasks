using Homework3_LiudvynskyiV.S.Models;

namespace Homework3_LiudvynskyiV.S.Services;

public interface IStorageComparer
{
    bool IsEqual(Product product1, Product product2);
}