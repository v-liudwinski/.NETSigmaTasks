using Homework7_LiudvynskyiV.S;
using Homework7_LiudvynskyiV.S.Models;

var card = new Card
{
    Id = 0,
    Number = "378282246310005",
    CardType = CardType.AmericanExpress
};
var luna = new LunaAlgorithm(card);
luna.Print();