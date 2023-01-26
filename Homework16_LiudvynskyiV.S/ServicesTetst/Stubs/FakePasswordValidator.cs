using Homework16_LiudvynskyiV.S.Validators;

namespace ServicesTetst.Stubs;

public class FakePasswordValidator : IPasswordValidator
{
    public bool IsValid(string password)
    {
        return true;
    }
}