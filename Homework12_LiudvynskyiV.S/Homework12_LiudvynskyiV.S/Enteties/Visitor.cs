using Homework12_LiudvynskyiV.S.Enums;

namespace Homework12_LiudvynskyiV.S.Enteties;

public class Visitor
{
    public int Position { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public Status Status { get; set; }
    public int MinutesNearPaydesk { get; set; }

    public override string ToString()
    {
        return $"{nameof(Position)}: {Position} | {nameof(Name)}: {Name} | {nameof(Age)}: {Age} |" +
               $"{nameof(Status)}: {Status} | {nameof(MinutesNearPaydesk)}: {MinutesNearPaydesk} |";
    }
}