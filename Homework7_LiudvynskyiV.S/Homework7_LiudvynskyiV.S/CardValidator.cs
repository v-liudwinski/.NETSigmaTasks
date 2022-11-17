using Homework7_LiudvynskyiV.S.Models;

namespace Homework7_LiudvynskyiV.S;

public class CardValidator
{
    private readonly Card _card;

    public CardValidator(Card card)
    {
        _card = card;
    }

    public bool IsCardValid()
    {
        if (!ValidateCardNumber()) return false;

        var isValid = false;
        switch (_card.CardType)
        {
            case CardType.AmericanExpress:
                if (_card.Number.Length == 15 
                    && string.Join("", _card.Number.Take(2)) is "34" or "37")
                {
                    isValid = true;
                }
                break;
            case CardType.MasterCard:
                if (_card.Number.Length == 16 
                    && string.Join("", _card.Number.Take(2)) is "51" or "52" or "53" or "54" or "55")
                {
                    isValid = true;
                }
                break;
            case CardType.Visa:
                if (_card.Number.Length is 13 or 16
                    && string.Join("", _card.Number.Take(2)) is "4")
                {
                    isValid = true;
                }
                break;
            default:
                return false;
        }

        return isValid;
    }

    private bool ValidateCardNumber()
    {
        var isValid = true;
        foreach (var digit in _card.Number)
        {
            if (int.TryParse(digit.ToString(), out var num)) continue;
            isValid = false;
            break;
        }

        return isValid;
    }
}