using System.Net.Mail;

namespace Homework16_LiudvynskyiV.S.Validators;

public class EmailValidator : IEmailValidator
{
    public bool IsEmailValid(string email)
    {
        try
        {
            var mailAddress = new MailAddress(email);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}