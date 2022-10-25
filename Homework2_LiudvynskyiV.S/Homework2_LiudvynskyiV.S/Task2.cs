using System.Drawing;
using System.Net;

namespace Homework2_LiudvynskyiV.S;

public enum Colors
{
    Red = 1,
    Orange,
    Yellow,
    Green,
    Blue,
    DarkBlue,
    Violet,
    Purple,
    Pink,
    Silver,
    Sand,
    DarkYellow,
    Brown,
    Grey,
    Black,
    White
}

public class Task2
{
    private readonly Colors[][] _pixels;

    public Task2(Colors[][] pixels)
    {
        _pixels = pixels;
    }

    public string MaxLineWithStartEndIndexes()
    {
        var line = _pixels.Single(x => 
            x.Length == _pixels
                .Select(l => l.Length)
                .Max())
            .ToList();

        var lineIndex = _pixels.Select((x, i) =>
        {
            if (x.Length == line.Capacity)
            {
                return i;
            }

            return -1;
        }).First(x => x >= 0);
        
        var lineStr = string.Join(", ", line);
        
        var indexFirst = line.Select((x, i) => i).First();
        var indexLast = line.Select((x, i) => i).Last();
        var lineLength = line.Count();

        var lineColorInt = line.Sum(x => x.GetHashCode()) % 16;
        var lineColor = Enum.GetName(typeof(Colors), lineColorInt);
        
        return string.Join(" ", "Pixels:", lineStr, "|", "Index of first element:", lineIndex, indexFirst, 
            "|", "Index of last element", lineIndex, indexLast, "|", "Line length:", lineLength, 
            "|", "Line Color:", lineColor);
    }
}