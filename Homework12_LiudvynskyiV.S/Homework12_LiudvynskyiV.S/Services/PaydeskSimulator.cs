using Homework12_LiudvynskyiV.S.Enteties;

namespace Homework12_LiudvynskyiV.S.Services;

public class PaydeskSimulator
{
    private readonly List<Paydesk> _paydesks;
    private readonly FileHandler _fileHandler;

    public PaydeskSimulator(FileHandler fileHandler)
    {
        _paydesks = GeneratePaydesks();
        _fileHandler = fileHandler;
    }

    public void StartPaydesksOperations()
    {
        while (_paydesks.All(x => x.Visitors.Count == 0))
        {
            foreach (var paydesk in _paydesks.Where(paydesk => paydesk.Visitors.Count > 5))
            {
                paydesk.Visitors.RemoveRange(0, 5);
            }

            foreach (var paydesk in _paydesks)
            {
                paydesk.Visitors.
                    AddRange(_fileHandler
                        .GetVisitors()
                        .Where(x => x.Position == paydesk.Position)
                    );
            }
        }
        Console.WriteLine("Operated visitors");
        foreach (var visitor in _fileHandler.GetVisitors())
        {
            Console.WriteLine(visitor.ToString());
        }
    }
    
    private List<Paydesk> GeneratePaydesks()
    {
        return new List<Paydesk>
        {
            new Paydesk
            {
                Position = 1
            },
            new Paydesk
            {
                Position = 2
            },
            new Paydesk
            {
                Position = 3
            },
            new Paydesk
            {
                Position = 4
            },
            new Paydesk
            {
                Position = 5
            },
            new Paydesk
            {
                Position = 6
            },
            new Paydesk
            {
                Position = 7
            },
            new Paydesk
            {
                Position = 8
            },
            new Paydesk
            {
                Position = 9
            },
            new Paydesk
            {
                Position = 10
            },
        };
    }
}