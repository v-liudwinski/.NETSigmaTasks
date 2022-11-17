using System.Runtime.InteropServices;
using Homework7_LiudvynskyiV.S.Models;

namespace Homework7_LiudvynskyiV.S;

public class LunaAlgorithm
{
    private readonly Card _card;

    public LunaAlgorithm(Card card)
    {
        _card = card;
    }

    private bool LunaValidation()
    {
        var cardValidator = new CardValidator(_card);
        if (!cardValidator.IsCardValid()) return false;

        var cardNumber = _card.Number
            .Select(x => int.Parse(x.ToString()))
            .ToArray();

        for (var i = 0; i < cardNumber.Length; i++)
        {
            i++;
            if (i > cardNumber.Length - 1) break;
            cardNumber[i] *= 2;
            if (cardNumber[i].ToString().Length == 2)
            {
                cardNumber[i] = cardNumber[i].ToString()
                    .Select(x => int.Parse(x.ToString()))
                    .Sum();
            }
        }

        var sum = cardNumber.Sum();

        return sum % 10 == 0;
    }

    public void Print()
    {
        if (LunaValidation())
        {
            Console.WriteLine(_card.Number);
            Console.WriteLine(_card.CardType);
        }
        else
        {
            Console.WriteLine("INVALID DATA!");
        }
    }
}