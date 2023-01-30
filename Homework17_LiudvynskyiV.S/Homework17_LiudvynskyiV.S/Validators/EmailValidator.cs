using System.Net.Mail;

namespace Homework17_LiudvynskyiV.S.Validators;

public class EmailValidator : IUserEmailValidator
{
    public bool IsValidEmail(string email)
    {
        try
        {
            var m = new MailAddress(email);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}